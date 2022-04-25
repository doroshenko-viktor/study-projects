import fs, { PathLike } from "fs";

export function appendContentToFile(): Promise<void> {
  return new Promise((resolve, reject) => {
    fs.appendFile("./file.txt", "content", (err) => {
      if (err) {
        reject(err);
        console.log(err);
      }
      return resolve();
    });
  });
}

export function writeContentToFile(
  path: PathLike,
  content: string
): Promise<void> {
  return new Promise((resolve, reject) => {
    fs.open(path, "w", (err, fd) => {
      if (err) {
        console.error(err);
        reject(err);
      }

      fs.write(fd, content, (err, numberWritten, str) => {
        if (err) {
          console.error(err);
          reject(err);
        }
        console.log(`number written: ${numberWritten}, str: ${str}`);
        resolve();
      });
    });
  });
}

export function writeContentToFile1(path: PathLike, content: string) {
  fs.writeFile(
    path,
    content,
    {
      encoding: "utf-8",
    },
    (err) => {
      console.error(err);
    }
  );
}

export function readFileStream(path: PathLike) {
  const stream = fs.createReadStream(path, {
    encoding: "utf-8",
  });

  stream.on("data", (data: string | Buffer) => {
    console.log(data);
  });
}

export function writeFileStream(path: PathLike) {
  const stream = fs.createWriteStream(path, {
    encoding: "utf-8",
  });

  for (let x = 0; x < 100; x++) {
    stream.write(`${x}\n`, (err) => {
      console.error(err);
    });
  }
}
