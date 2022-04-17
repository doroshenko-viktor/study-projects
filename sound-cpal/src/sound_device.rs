use cpal::traits::{DeviceTrait, HostTrait};
use cpal::HostId;

pub fn list_devices() {
    let result = cpal::available_hosts();

    let device_names = result
        .iter()
        .map(|x| x.name())
        .collect::<Vec<&str>>()
        .join("; ");

    println!("{device_names}");
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn list_devices_test() {
        list_devices();
    }
}
