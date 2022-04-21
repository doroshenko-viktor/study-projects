use std::{fs::File, io::BufReader, sync::mpsc, thread};

use cpal::{
    traits::{DeviceTrait, HostTrait, StreamTrait},
    HostId, Stream, StreamConfig,
};

pub fn play() {
    // let host = cpal::host_from_id(HostId::CoreAudio).unwrap();
    let host = cpal::default_host();
    let device = host.default_output_device().unwrap();
    let config = device.default_output_config().unwrap();

    let sample_format = config.sample_format();
    let config = StreamConfig::from(config);

    let stream = match sample_format {
        cpal::SampleFormat::I16 => get_stream::<i16>(&device, config),
        cpal::SampleFormat::U16 => get_stream::<u16>(&device, config),
        cpal::SampleFormat::F32 => get_stream::<f32>(&device, config),
    };
    stream.play().unwrap();

    std::thread::sleep(std::time::Duration::from_millis(2000));
}

pub fn play_from_file() {
    let host = cpal::default_host();
    let device = host.default_output_device().unwrap();
    let config = device.default_output_config().unwrap();
    let channels = config.channels() as usize;

    const PATH: &str = concat!(env!("CARGO_MANIFEST_DIR"), "/recorded.wav");

    let file = File::open(PATH).unwrap();
    let mut wav_reader = hound::WavReader::new(BufReader::new(file)).unwrap();

    let (tx, rx) = mpsc::channel();

    let stream = device
        .build_output_stream(
            &config.into(),
            move |data: &mut [f32], _| {
                let mut s_ref = wav_reader.samples::<f32>();

                for frame in data.chunks_mut(channels) {
                    match &s_ref.next() {
                        Some(Ok(ref x)) => {
                            let value = cpal::Sample::from::<f32>(x);
                            for sample in frame.iter_mut() {
                                *sample = value;
                            }
                        }
                        _ => {
                            tx.send("finished").unwrap();
                            break;
                        }
                    }
                }
            },
            |err| eprintln!("an error occurred on stream: {}", err),
        )
        .unwrap();

    stream.play().expect("error on play");

    let res = rx.recv().unwrap();
    dbg!(format!("Received: {res}"));

    drop(stream)
}

fn get_stream<T>(device: &cpal::Device, config: StreamConfig) -> Stream
where
    T: cpal::Sample,
{
    let err_fn = |err| eprintln!("an error occurred on stream: {}", err);

    let mut sample_clock = 0f32;
    let mut freq = 100f32;
    let mut direction = true;

    let mut source = move || {
        sample_clock = (sample_clock + 0.5f32) % config.sample_rate.0 as f32;

        if direction {
            freq = freq + 1f32;
        } else {
            freq = freq - 1f32;
        }

        direction = if freq > 500f32 || freq < 100f32 {
            !direction
        } else {
            direction
        };

        (sample_clock * freq * 2.0 * std::f32::consts::PI / config.sample_rate.0 as f32).sin()
    };

    let stream = device
        .build_output_stream(
            &config,
            move |data: &mut [T], _| {
                for frame in data.chunks_mut(config.channels as usize) {
                    let value: T = cpal::Sample::from::<f32>(&source());
                    for sample in frame.iter_mut() {
                        *sample = value;
                    }
                }
            },
            err_fn,
        )
        .unwrap();

    stream
}

#[test]
fn play_test() {
    play()
}

#[test]
fn play_from_file_test() {
    play_from_file()
}
