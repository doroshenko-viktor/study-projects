use std::net::SocketAddr;

use tokio::{
    io::{AsyncBufReadExt, AsyncReadExt, AsyncWriteExt, BufReader},
    net::TcpListener,
    sync::broadcast,
};

#[tokio::main]
async fn main() {
    println!("starting...");
    let listener = TcpListener::bind("localhost:8080").await.unwrap();
    let (tx, _rx) = broadcast::channel::<(String, SocketAddr)>((10));
    loop {
        let (mut socket, connection_addr) = listener.accept().await.unwrap();
        let tx = tx.clone();
        let mut rx = tx.subscribe();
        tokio::spawn(async move {
            let (reader, mut writer) = socket.split();
            let mut reader = BufReader::new(reader);
            let mut line = String::new();

            loop {
                tokio::select! {
                    result = reader.read_line(&mut line) => {
                        if result.unwrap() == 0 {
                            break;
                        }
                        tx.send((line.clone(), connection_addr.clone())).unwrap();
                        line.clear();
                    },
                    result = rx.recv() => {
                        let (message, sender_addr) = result.unwrap();
                        if sender_addr != connection_addr {
                            writer.write_all(&message.as_bytes()).await.unwrap();
                        }
                    }
                }
            }
        });
    }
}
