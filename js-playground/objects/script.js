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

function usingThis() {
    const obj = {
        key1: "value1",
        printValue() {
            console.log(this);
            console.log(`key1: ${this.key1}`);
        }
    };

    obj.printValue(); // here `this` refers to the object `obj` on which it was called. `this` is `obj`
    let { printValue } = obj;
    printValue(); // here `printValue` has been called in the context of `window` object, which means that here `this` is `window`
    // it is possible to bind necessary value to `this` with bind
    printValue = printValue.bind(obj);
    printValue(); // here `this` is again `obj`
}

function thisOnArrowFunctions() {
    const obj = {
        key1: "value1",
        methodFunction() {
            console.log(`key1 from methodFunction: ${this.key1}`);
            const internalArrowFunc = () => {
                console.log(`key1 from internalArrowFunc: ${this.key1}`);
            }
            internalArrowFunc();
        },
        arrowFunc: () => {
            console.log(`key1 from arrowFunc: ${this.key1}`);
        }
    };

    obj.methodFunction();
    obj.arrowFunc();
}

thisOnArrowFunctions();
// usingThis();
// iteratingObject();
// usingClass();