pub mod SoundACL {
    use cpal::{
        traits::{DeviceTrait, HostTrait},
        Device, Host, HostId,
    };
    use std::fmt::Display;

    pub struct SoundIO {
        pub input: Option<Device>,
        pub output: Option<Device>,
    }

    impl Display for SoundIO {
        fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
            let input = self.input.as_ref().map_or("default".to_string(), |x| {
                x.name().map_or("default".to_string(), |x| x)
            });
            let output = self.output.as_ref().map_or("default".to_string(), |x| {
                x.name().map_or("default".to_string(), |x| x)
            });

            write!(f, "input: {input}; output: {output}")
        }
    }

    pub fn list_devices() -> Vec<HostId> {
        let devices = cpal::available_hosts();
        return devices;
    }

    pub fn get_audio_host(device_id: HostId) -> Result<Host, &'static str> {
        return match cpal::host_from_id(device_id) {
            Ok(device) => Ok(device),
            _ => Err("Error happened constructing audio device: {}"),
        };
    }

    pub fn get_audio_io(device: &Host) -> SoundIO {
        return SoundIO {
            input: device.default_input_device(),
            output: device.default_output_device(),
        };
    }

    pub fn print_device_config(device: &Device) {
        println!("{:?}", device.name().unwrap());

        if let Ok(config) = device.default_input_config() {
            println!("input config: {:?}", config);
        }
        if let Ok(config) = device.default_output_config() {
            println!("output config: {:?}", config);
        }
    }

    #[cfg(test)]
    mod tests {
        use super::*;

        #[test]
        fn print_device_config_test() {
            let host = get_audio_host(HostId::CoreAudio).unwrap();
            let io = get_audio_io(&host);
            print_device_config(&io.input.unwrap());
            print_device_config(&io.output.unwrap());
        }

        #[test]
        fn get_audio_io_test() {
            let device = get_audio_host(HostId::CoreAudio).unwrap();
            let io = get_audio_io(&device);
            println!("{io}");
        }

        #[test]
        fn get_device_test() {
            let device = get_audio_host(HostId::CoreAudio).unwrap();
            assert_eq!(device.id(), HostId::CoreAudio);
        }

        #[test]
        fn list_devices_test() {
            let devices = list_devices();

            let device_names = devices
                .iter()
                .map(|x| x.name())
                .collect::<Vec<&str>>()
                .join("; ");

            println!("{device_names}");

            // this will work only for Mac Os
            assert_eq!(devices.len(), 1);
            assert_eq!(devices[0], HostId::CoreAudio);
        }
    }
}
