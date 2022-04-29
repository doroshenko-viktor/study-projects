import { makeRawHttpRequest } from "./http";

describe("http tests", () => {
  it("should make request", async () => {
    const result = await makeRawHttpRequest<object>(
      "jsonplaceholder.typicode.com",
      "/posts"
    );
    console.log(result);
  });
});
