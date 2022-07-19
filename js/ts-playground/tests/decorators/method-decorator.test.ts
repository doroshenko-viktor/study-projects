import { bench } from "../../src/decorators/method-decorator";

class TestClass {
  @bench
  public async testMethod(): Promise<void> {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve();
      }, 200);
    });
  }
}

describe("method decorator", () => {
  it("benchmark should log execution time", async () => {
    // arrange
    const instance = new TestClass();
    // act
    const result = await instance.testMethod();
    // assert
  });
});
