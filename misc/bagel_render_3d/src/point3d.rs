use std::ops::{Add, Div, Mul, Sub};

use crate::point2d::Point2D;

pub struct Point3D {
    pub x: f64,
    pub y: f64,
    pub z: f64,
}

impl Point3D {
    pub fn new(x: f64, y: f64, z: f64) -> Self {
        Point3D { x, y, z }
    }

    pub fn from_point2d(x: f64, uv: &Point2D) -> Self {
        Point3D {
            x,
            y: uv.x,
            z: uv.y,
        }
    }

    pub fn len(&self) -> f64 {
        (self.x.powi(2) * self.y.powi(2) + self.y.powi(2)).sqrt()
    }

    pub fn norm(&self) -> Self {
        let length = self.len();
        Point3D {
            x: self.x / length,
            y: self.y / length,
            z: self.z / length,
        }
    }
}

impl Add<Point3D> for Point3D {
    type Output = Point3D;

    fn add(self, rhs: Point3D) -> Self::Output {
        Point3D {
            x: self.x + rhs.x,
            y: self.y + rhs.y,
            z: self.z + rhs.z,
        }
    }
}

impl Sub<Point3D> for Point3D {
    type Output = Point3D;

    fn sub(self, rhs: Point3D) -> Self::Output {
        Point3D {
            x: self.x - rhs.x,
            y: self.y - rhs.y,
            z: self.z - rhs.z,
        }
    }
}

impl Div<Point3D> for Point3D {
    type Output = Point3D;

    fn div(self, rhs: Point3D) -> Self::Output {
        Point3D {
            x: self.x - rhs.x,
            y: self.y - rhs.y,
            z: self.z - rhs.z,
        }
    }
}

impl Mul<Point3D> for Point3D {
    type Output = Point3D;

    fn mul(self, rhs: Point3D) -> Self::Output {
        Point3D {
            x: self.x * rhs.x,
            y: self.y * rhs.y,
            z: self.z * rhs.z,
        }
    }
}
