use serde::{Deserialize, Serialize};
use serde_json::Error as SerdeError;

fn rename_all() -> Result<String, SerdeError> {
    #[derive(Deserialize, Serialize)]
    // #[serde(rename = "new_name")]
    #[serde(rename_all = "PascalCase")]
    struct T {
        pub some_key_1: String,
        pub some_key_2: u8,
    }

    let json = "{
        \"SomeKey1\": \"v1\",
        \"SomeKey2\": 8
    }";

    let t: T = serde_json::from_str(&json)?;
    let result_json = serde_json::to_string(&t)?;

    Ok(result_json)
}

#[cfg(test)]
mod tests {
    use super::rename_all;

    #[test]
    pub fn test_rename() {
        let result = rename_all();
        match result {
            Ok(res) => {
                dbg!(&res);
            }
            Err(err) => {
                println!("abcd ERROR: {}", err)
            }
        }
    }
}
