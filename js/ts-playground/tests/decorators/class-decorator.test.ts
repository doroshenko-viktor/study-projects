import { Frozen as Sealed } from "../../src/decorators/class-decorator";

@Sealed
class TestClass {
  public field = 1;
}

class TestClassExt extends TestClass {
  public field2 = 1;
}

describe("class decorator tests", () => {
  it("should freeze instance of TestClass", () => {
    // arrange
    // const i1 = new TestClassExt();
    // act
    const res = Object.isFrozen(TestClass);
    // assert
    expect(res).toBeTruthy();
  });
});
