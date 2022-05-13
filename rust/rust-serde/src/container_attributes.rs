use serde::{Deserialize, Serialize};
use serde_json::Error as SerdeError;

fn rename_all() -> Result<String, SerdeError> {
    #[derive(Deserialize, Serialize, Debug)]
    #[serde(rename_all = "camelCase")]
    struct T {
        pub some_key_1: String,
        pub some_key_2: u8,
    }

    let json = "{
        \"someKey1\": \"v1\",
        \"someKey2\": 8
    }";

    let t: T = serde_json::from_str(&json)?;
    dbg!(&t);
    let result_json = serde_json::to_string(&t)?;
    dbg!(&result_json);

    Ok(result_json)
}

#[cfg(test)]
mod tests {
    use super::rename_all;

    #[test]
    pub fn test_rename() {
        let result = rename_all();
        if let Err(err) = result {
            println!("abcd ERROR: {}", err)
        }
    }
}
