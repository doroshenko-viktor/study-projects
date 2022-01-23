use std::fmt::Display;

#[derive(Debug)]
pub struct Screen {
    width: i64,
    height: i64,
    aspect: f64,

    field: Vec<char>,
}

impl Screen {
    pub fn new(width: i64, height: i64, char_aspect: f64) -> Self {
        let aspect = width as f64 / height as f64;
        let screen_capacity = (width * height + height) as usize;
        let mut field: Vec<char> = Vec::with_capacity(screen_capacity);

        for ord in 0..height {
            let ord = get_normalized_ordinate(ord, height);
            for absc in 0..width {
                let absc = get_normalized_absciss(absc, width, aspect, char_aspect);

                if (ord.powi(2) + absc.powi(2)) < 0.9 {
                    field.push('@');
                } else {
                    field.push(' ');
                }
            }
            field.push('\n');
        }

        Screen {
            width,
            height,
            aspect,
            field,
        }
    }
}

fn get_normalized_ordinate(ord: i64, height: i64) -> f64 {
    ord as f64 / height as f64 * 2.0 - 1.0
}

fn get_normalized_absciss(absc: i64, width: i64, aspect: f64, char_aspect: f64) -> f64 {
    (absc as f64 / width as f64 * 2.0 - 1.0) * aspect * char_aspect
}

impl Display for Screen {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        let str: String = self.field.iter().collect();
        write!(f, "{}", str)
    }
}
