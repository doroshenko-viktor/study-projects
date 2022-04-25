import {
  readFileStream,
  writeContentToFile,
  appendContentToFile,
  writeFileStream,
} from "./fs";
import fs from "node:fs";

describe("fs tests::appendContentToFile", () => {
  it("should create file and append content", async () => {
    await appendContentToFile();
    const buf = fs.readFileSync("./file.txt");
    const content = buf.toString("utf8");
    const result = content.match(/content/);
    expect(result && result.length > 0).toBeTruthy();
  });
});

describe("fs::writeContentToFile tests", () => {
  it("should create file and write content", async () => {
    await writeContentToFile("./file-write.txt", "WRITE_CONTENT");
  });
});

describe("fs::readFileStream tests", () => {
  it("should read file content", async () => {
    await readFileStream("./file.txt");
  });
});

describe("fs::writeFileStream tests", () => {
  it("should write file content", async () => {
    await writeFileStream("./file-stream-write.txt");
  });
});
