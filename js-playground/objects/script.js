class TestClass {
    constructor(key1, key2) {
        this.key1 = key1;
        this.key2 = key2;
    }

    toString() {
        return `{ key1: ${this.key1}; key2: ${this.key2}; }`;
    }
}

function usingClass() {
    const testClass = new TestClass("value1", "value2");
    console.table(testClass);
    console.log(testClass);
    console.log(`TestClass instance: ${testClass}`);
}

function iteratingObject() {
    const obj = {
        key1: "value1",
        key2: "value2",
        key3: "value3",
        toString() {
            return `key1: ${this.key1}; key2: ${this.key2}; key3: ${this.key3} `;
        }
    };

    // console.table(obj);
    console.log(obj);
    console.log(obj.toString());

    for (const key in obj) {
        console.log(`key: ${key} => ${obj[key]} `);
    }
}

// iteratingObject();
// usingClass();