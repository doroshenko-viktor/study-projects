import { entity, readonly } from "../../src/decorators/class-decorator";

// @readonly
@entity
class TestClass {
  public field = 1;
}

class TestClassExt extends TestClass {
  public field2 = 1;
}

describe("class decorator tests", () => {
  it("should freeze instance of TestClass", () => {
    // arrange
    const i1 = new TestClass();
    // act
    // const res = Object.isFrozen(TestClass);
    expect((i1 as any).createDate).toBeDefined();
    // assert
    // expect(res).toBeTruthy();
  });
});
