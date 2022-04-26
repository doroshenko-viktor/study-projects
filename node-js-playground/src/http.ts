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
      return res.writeHead(400, "Bad request");
    }
    console.log(process.cwd());
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
