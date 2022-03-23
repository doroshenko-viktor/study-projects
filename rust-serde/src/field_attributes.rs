use serde::{Deserialize, Serialize};
use serde_json::Error as SerializationError;

fn rename() -> Result<String, SerializationError> {
    #[derive(Deserialize, Serialize, Debug)]
    struct T {
        #[serde(rename(serialize = "SERIALIZE_KEY", deserialize = "other_KEY"))]
        pub some_key_1: String,
        #[serde(rename = "key2")]
        pub some_key_2: u8,
    }

    let json = "{
        \"other_KEY\": \"v1\",
        \"key2\": 8
    }";

    let t: T = serde_json::from_str(&json)?;
    dbg!(&t);
    let serialized = serde_json::to_string(&t)?;
    dbg!(&serialized);
    Ok(serialized)
}

#[cfg(test)]
mod tests {
    use super::rename;

    #[test]
    fn test() {
        if let Err(err) = rename() {
            println!("ERROR: {}", err)
        }
    }
}
