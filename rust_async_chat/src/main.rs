use tokio::{
    io::{AsyncBufReadExt, AsyncReadExt, AsyncWriteExt, BufReader},
    net::TcpListener,
};

#[tokio::main]
async fn main() {
    println!("starting...");
    let listener = TcpListener::bind("localhost:8080").await.unwrap();
    loop {
        let (mut socket, _) = listener.accept().await.unwrap();

        tokio::spawn(async move {
            let (read, mut write) = socket.split();
            let mut reader = BufReader::new(read);
            let mut line = String::new();

            loop {
                let bytes_read = reader.read_line(&mut line).await.unwrap();
                if bytes_read == 0 {
                    break;
                }
                write.write_all(&line.as_bytes()).await.unwrap();
                line.clear();
            }
        });
    }
}
