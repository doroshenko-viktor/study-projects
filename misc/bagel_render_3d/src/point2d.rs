use std::ops::{Add, Div, Mul, Sub};

pub struct Point2D {
    pub x: f64,
    pub y: f64,
}

impl Point2D {
    pub fn new(x: f64, y: f64) -> Self {
        Point2D { x, y }
    }

    pub fn len(&self) -> f64 {
        (self.x.powi(2) * self.y.powi(2)).sqrt()
    }
}

impl Add<Point2D> for Point2D {
    type Output = Point2D;

    fn add(self, rhs: Point2D) -> Self::Output {
        Point2D {
            x: self.x + rhs.x,
            y: self.y + rhs.y,
        }
    }
}

impl Sub<Point2D> for Point2D {
    type Output = Point2D;

    fn sub(self, rhs: Point2D) -> Self::Output {
        Point2D {
            x: self.x - rhs.x,
            y: self.y - rhs.y,
        }
    }
}

impl Div<Point2D> for Point2D {
    type Output = Point2D;

    fn div(self, rhs: Point2D) -> Self::Output {
        Point2D {
            x: self.x - rhs.x,
            y: self.y - rhs.y,
        }
    }
}

impl Mul<Point2D> for Point2D {
    type Output = Point2D;

    fn mul(self, rhs: Point2D) -> Self::Output {
        Point2D {
            x: self.x * rhs.x,
            y: self.y * rhs.y,
        }
    }
}
