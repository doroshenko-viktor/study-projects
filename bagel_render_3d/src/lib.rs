mod options;
mod point2d;
mod point3d;
mod screen;
use crate::{options::ConsoleOptions, screen::Screen};
use terminal_size;

pub fn run() {
    let (width, height) = terminal_size::terminal_size().unwrap();
    let console_options: ConsoleOptions = ConsoleOptions::new(width.0, height.0);
    let mut screen = Screen::new(console_options.width, console_options.height, 0.5);
    screen.run()
}
