use crate::{point2d::Point2D, point3d::Point3D};
use std::{fmt::Display, thread, time::Duration};

const GRADIENT: [char; 9] = ['r', '/', '(', 'l', 'H', 'W', '8', '$', '@'];

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
                // let absc = absc + (frame_ind as f64 * 0.01).sin();

                let uv = Point2D::new(absc, ord);
                let ro = Point3D::new(-2 as f64, 0 as f64, 0 as f64);
                let rd = Point3D::from_point2d(1 as f64, &uv).norm();

                let mut color: i32 = 0;
                let intersection = sphere(ro, rd, 1.0);
                if intersection.x > 0 as f64 {
                    color = (GRADIENT.len() - 1) as i32;
                }
                // if (intersection.x.powi(2) + intersection.y.powi(2)) < 0.5 {
                // let dist = (uv.x.powi(2) + uv.y.powi(2)).sqrt();
                // let mut color_ind = (1.0 / dist) as usize;
                let color = clamp(color, 0, (GRADIENT.len() - 1) as i32);
                // if color_ind >= GRADIENT.len() {
                //     color_ind = GRADIENT.len() - 1;
                // }
                let pixel = GRADIENT.get(color as usize).unwrap_or(&'.').clone();
                field.push(pixel);
                // } else {
                //     field.push('.');
                // }
            }
        }
        self.field = field;
    }
}

fn clamp(value: i32, min: i32, max: i32) -> i32 {
    i32::max(i32::min(value, max), min)
}

fn dot(a: &Point3D, b: &Point3D) -> f64 {
    a.x * b.x + a.y * b.y + a.z * b.z
}

fn sphere(ro: Point3D, rd: Point3D, r: f64) -> Point2D {
    let b = dot(&ro, &rd);
    let c = dot(&ro, &ro) - r.powi(2);
    let h = b.powi(2) - c;
    if h < 0 as f64 {
        return Point2D::new(-1.0, -1.0);
    }
    let h = h.powi(2);
    return Point2D::new(-b - h, -b + h);
}

fn get_pixel(point: Point2D) -> char {
    let dist = (point.x.powi(2) + point.y.powi(2)).sqrt();
    let mut color_ind = (1.0 / dist) as usize;
    if color_ind >= GRADIENT.len() {
        color_ind = GRADIENT.len() - 1;
    }
    GRADIENT.get(color_ind).unwrap_or(&'.').clone()
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
