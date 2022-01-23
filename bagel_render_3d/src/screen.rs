use std::{fmt::Display, thread, time::Duration};

const GRADIENT: [char; 16] = [
    ' ', '.', '!', '/', 'r', '(', 'l', '1', 'Z', '4', 'H', '9', 'W', '8', '$', '@',
];

#[derive(Debug)]
pub struct Screen {
    width: u16,
    height: u16,
    aspect: f64,
    char_aspect: f64,
    field: Vec<char>,
}

impl Screen {
    pub fn new(width: u16, height: u16, char_aspect: f64) -> Self {
        let aspect = width as f64 / height as f64;
        let screen_capacity = (width * height) as usize;
        let field: Vec<char> = Vec::with_capacity(screen_capacity);

        Screen {
            width,
            height,
            aspect,
            char_aspect,
            field,
        }
    }

    pub fn run(&mut self) {
        for frame_ind in 0..10000 {
            self.fill_circle(frame_ind);
            print!("{}", self);
            thread::sleep(Duration::from_millis(41));
        }
    }

    pub fn fill_circle(&mut self, frame_ind: i64) {
        let screen_capacity = (self.width * self.height) as usize;
        let mut field: Vec<char> = Vec::with_capacity(screen_capacity);

        for ord in 0..self.height - 1 {
            let ord = get_normalized_ordinate(ord, self.height);
            for absc in 0..self.width {
                let absc = get_normalized_absciss(absc, self.width, self.aspect, self.char_aspect);
                let absc = absc + (frame_ind as f64 * 0.01).sin();

                if (ord.powi(2) + absc.powi(2)) < 0.5 {
                    let dist = (ord.powi(2) + absc.powi(2)).sqrt();
                    let mut color_ind = (1.0 / dist) as usize;
                    if color_ind > GRADIENT.len() {
                        color_ind = GRADIENT.len() - 1;
                    }
                    let pixel = GRADIENT.get(color_ind).unwrap_or(&'.');
                    field.push(pixel.clone());
                } else {
                    field.push('.');
                }
            }
        }
        self.field = field;
    }
}

fn get_normalized_ordinate(ord: u16, height: u16) -> f64 {
    ord as f64 / height as f64 * 2.0 - 1.0
}

fn get_normalized_absciss(absc: u16, width: u16, aspect: f64, char_aspect: f64) -> f64 {
    (absc as f64 / width as f64 * 2.0 - 1.0) * aspect * char_aspect
}

impl Display for Screen {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        let str: String = self.field.iter().collect();
        write!(f, "{}", str)
    }
}
