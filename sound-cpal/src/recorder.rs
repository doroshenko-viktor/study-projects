use std::{
    fs::File,
    io::BufWriter,
    sync::{Arc, Mutex},
};

use cpal::{
    traits::{DeviceTrait, HostTrait, StreamTrait},
    HostId, Stream,
};
use hound;

pub fn record() {
    let host = cpal::host_from_id(HostId::CoreAudio).unwrap();
    let device = host.default_input_device().unwrap();
    let config = device.default_input_config().unwrap();

    const PATH: &str = concat!(env!("CARGO_MANIFEST_DIR"), "/recorded.wav");

    let wav_spec = hound::WavSpec {
        channels: config.channels().clone(),
        sample_rate: config.sample_rate().0.clone(),
        bits_per_sample: (&config.sample_format().sample_size() * 8) as u16,
        sample_format: match &config.sample_format() {
            cpal::SampleFormat::I16 => hound::SampleFormat::Int,
            cpal::SampleFormat::U16 => hound::SampleFormat::Int,
            cpal::SampleFormat::F32 => hound::SampleFormat::Float,
        },
    };
    println!("config: {:?}", wav_spec);

    let wav_writer = Arc::new(Mutex::new(Some(
        hound::WavWriter::create(PATH, wav_spec).unwrap(),
    )));
    let wav_writer_2 = wav_writer.clone();
    let stream = get_stream(wav_writer_2, config, device);

    stream.play().expect("error on record");

    std::thread::sleep(std::time::Duration::from_secs(10));

    drop(stream);
    wav_writer
        .lock()
        .unwrap()
        .take()
        .unwrap()
        .finalize()
        .expect("error on wav writer close");
    println!("Recording {} complete!", PATH);
}

fn get_stream(
    writer: WavWriterHandle,
    config: cpal::SupportedStreamConfig,
    device: cpal::Device,
) -> Stream {
    let err_fn = move |err| {
        eprintln!("an error occurred on stream: {}", err);
    };
    return match config.sample_format() {
        cpal::SampleFormat::F32 => device
            .build_input_stream(
                &config.into(),
                move |data, _: &_| write_input_data::<f32, f32>(data, &writer),
                err_fn,
            )
            .unwrap(),
        cpal::SampleFormat::I16 => device
            .build_input_stream(
                &config.into(),
                move |data, _: &_| write_input_data::<i16, i16>(data, &writer),
                err_fn,
            )
            .unwrap(),
        cpal::SampleFormat::U16 => device
            .build_input_stream(
                &config.into(),
                move |data, _: &_| write_input_data::<u16, i16>(data, &writer),
                err_fn,
            )
            .unwrap(),
    };
}

type WavWriterHandle = Arc<Mutex<Option<hound::WavWriter<BufWriter<File>>>>>;

fn write_input_data<T, U>(input: &[T], writer: &WavWriterHandle)
where
    T: cpal::Sample,
    U: cpal::Sample + hound::Sample,
{
    if let Ok(mut guard) = writer.try_lock() {
        if let Some(writer) = guard.as_mut() {
            for &sample in input.iter() {
                // println!("sample: {:?}", sample.to_f32());
                let sample: U = cpal::Sample::from(&sample);
                writer.write_sample(sample).ok();
            }
        }
    }
}

#[test]
fn record_test() {
    record()
}
