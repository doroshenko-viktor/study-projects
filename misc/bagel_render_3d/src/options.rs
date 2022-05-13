pub struct ConsoleOptions {
    pub width: u16,
    pub height: u16,
}

impl ConsoleOptions {
    pub fn new(width: u16, height: u16) -> Self {
        ConsoleOptions { width, height }
    }
}
