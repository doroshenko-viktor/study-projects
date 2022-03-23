use serde::Deserialize;
use std::{io, path::Path};

#[derive(Deserialize, Debug)]
pub struct Model {
    pub menu: MenuModel,
}

#[derive(Deserialize, Debug)]
pub struct MenuModel {
    pub id: String,
    pub value: String,
    pub popup: PopupModel,
}

#[derive(Deserialize, Debug)]
pub struct PopupModel {
    #[serde(rename(deserialize = "menuitem"))]
    pub menu_item: Vec<MenuItemModel>,
}

#[derive(Deserialize, Debug)]
pub struct MenuItemModel {
    pub value: String,
    #[serde(rename(deserialize = "onclick"))]
    pub on_click: String,
}

#[derive(Debug)]
pub enum ParsingError {
    IO(io::Error),
    Parse(serde_json::Error),
}

impl From<io::Error> for ParsingError {
    fn from(e: io::Error) -> Self {
        ParsingError::IO(e)
    }
}

impl From<serde_json::Error> for ParsingError {
    fn from(e: serde_json::Error) -> Self {
        ParsingError::Parse(e)
    }
}

pub fn load_model<T: AsRef<Path>>(path: T) -> Result<Model, ParsingError> {
    let content = std::fs::read_to_string(path)?;
    let model: Model = serde_json::from_str(&content)?;
    Ok(model)
}

#[cfg(test)]
mod tests {
    use crate::ParsingError;
    use super::load_model;

    #[test]
    fn load_json() -> Result<(), ParsingError> {
        let model = load_model("./assets/test.json")?;
        dbg!(model);
        Ok(())
    }
}
