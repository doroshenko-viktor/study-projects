import http, { IncomingMessage, ServerResponse } from "node:http";
import url, { UrlWithParsedQuery } from "node:url";
import fs from "node:fs";
import path from "node:path";

const getServedFileName = (requestPath: string) => {
  if (requestPath === "/") {
    return path.join("..", "public", "index.html");
  }
  return path.join("..", "public", `${requestPath}.html`);
};

const server = http.createServer(
  (req: IncomingMessage, res: ServerResponse) => {
    if (!req.url) {
      res.writeHead(400, "Bad request");
      return res.end();
    }

    const requestUrl: UrlWithParsedQuery = url.parse(req.url, true);
    const requestPath = requestUrl.pathname;
    console.info("serving: " + requestPath);

    if (!requestPath) {
      res.writeHead(400, "Bad request");
      return res.end();
    }

    const fileName = getServedFileName(requestPath);

    fs.readFile(fileName, (err, data) => {
      if (err) {
        res.writeHead(500, "Server error");
        console.error(err);
        return res.end();
      }

      res.writeHead(200, "OK");
      res.write(data, (err) => {
        if (err) {
          console.error(`Error serving request ${err}`);
        }
      });
      return res.end();
    });
  }
);

server.listen(8000, () => {
  console.log("listening on port 8000");
});

export function makeRawHttpRequest<T>(
  host: string,
  urlPath: string
): Promise<T> {
  const options = {
    hostname: host,
    path: urlPath,
    method: "GET",
  };

  return new Promise<T>((resolve, reject) => {
    http
      .request(options, (res) => {
        let data = "";

        res.on("data", (chunk) => {
          data += chunk;
        });

        res.on("end", () => {
          const result = JSON.parse(data) as T;
          console.log("Body:", result);
          resolve(result);
        });
      })
      .on("error", (err) => {
        console.log("Error: ", err);
        reject(err);
      })
      .end();
  });
}
