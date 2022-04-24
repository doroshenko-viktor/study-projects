use std::{
    borrow::Borrow,
    sync::{Arc, Mutex},
    thread,
};

use cpal::{
    traits::{DeviceTrait, HostTrait, StreamTrait},
    BufferSize, HostId, StreamConfig,
};
use ringbuf::RingBuffer;

pub fn reduce_noise() {
    let host = cpal::host_from_id(HostId::CoreAudio).unwrap();

    let input_device = host.default_input_device().unwrap();
    let output_device = host.default_output_device().unwrap();
    let mut config: StreamConfig = input_device.default_input_config().unwrap().into();

    config.buffer_size = BufferSize::Fixed(32);

    let buf = RingBuffer::<f32>::new(128);
    let (mut producer, mut consumer) = buf.split();

    for _ in 0..16 {
        producer.push(1.0).unwrap();
    }

    let input_stream = input_device
        .build_input_stream(
            &config,
            move |data: &[f32], x: &cpal::InputCallbackInfo| {
                // let x = data.len();
                // println!("input data: {x}");
                for &sample in data {
                    let result = producer.push(sample);
                    if let Err(err) = result {
                        eprintln!("Error reading stream: {err}");
                        continue;
                    }
                }
            },
            |err| eprintln!("Error on input stream {err}"),
        )
        .unwrap();

    let output_stream = output_device
        .build_output_stream(
            &config,
            move |data: &mut [f32], _| {
                // let x = data.len();
                // println!("output data: {x}");
                let mut previous_sample = 0f32;
                for sample in data {
                    *sample = match consumer.pop() {
                        Some(val) => {
                            previous_sample = val;
                            val
                        }
                        None => {
                            eprintln!("No sample to read");
                            previous_sample = previous_sample * 0.99;
                            previous_sample
                        }
                    }
                }
            },
            |err| eprintln!("Error on output stream {err}"),
        )
        .unwrap();

    input_stream.play().unwrap();
    output_stream.play().unwrap();

    std::thread::sleep(std::time::Duration::from_secs(300));
    drop(input_stream);
    drop(output_stream);
}

#[test]
pub fn test_reduce_noise() {
    reduce_noise()
}
