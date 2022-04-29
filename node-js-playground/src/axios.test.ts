import { makeGetRequest } from "./axios";

describe("axios tests", () => {
  it("should make request", async () => {
    await makeGetRequest();
  });
});
