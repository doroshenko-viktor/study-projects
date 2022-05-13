import * as axios from "./axios";

describe("axios tests", () => {
  it("should make request", async () => {
    await axios.makeGetRequest();
  });
  it("should make post request with client", async () => {
    await axios.makePostWithPreconfiguredClient();
  });
});
