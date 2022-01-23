pub struct ConsoleOptions {
    pub width: i64,
    pub height: i64,
}

impl ConsoleOptions {
    pub fn new(width: i64, height: i64) -> Self {
        ConsoleOptions { width, height }
    }
}