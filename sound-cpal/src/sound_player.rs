use std::{
    fs::File,
    io::{BufRead, BufReader},
    sync::{Arc, Mutex},
};

use cpal::{
    traits::{DeviceTrait, HostTrait, StreamTrait},
    HostId, Stream, StreamConfig,
};

pub fn play() {
    // let host = cpal::host_from_id(HostId::CoreAudio).unwrap();
    let host = cpal::default_host();
    let device = host.default_output_device().unwrap();
    let config = device.default_output_config().unwrap();

    const PATH: &str = concat!(env!("CARGO_MANIFEST_DIR"), "/recorded.wav");

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
