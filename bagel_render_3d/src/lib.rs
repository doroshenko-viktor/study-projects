mod options;
mod screen;
use crate::{options::ConsoleOptions, screen::Screen};

pub fn run() {
    let console_options: ConsoleOptions = ConsoleOptions::new(100, 50);
    let mut screen = Screen::new(console_options.width, console_options.height, 0.5);
    println!("{}", screen);
}
