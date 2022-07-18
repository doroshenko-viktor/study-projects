import { overwrite } from "../../src/decorators/class-field-decorator";

class TestClass {
  @overwrite
  public testField: number = 1;
}

describe("field decorator tests", () => {
  it("should overwrite field value", () => {
    const instance = new TestClass();
    expect(instance.testField).toBe("overwritten");
  });
});
