export const bench: MethodDecorator = (
  target,
  key,
  descriptor: PropertyDescriptor
) => {
  const original: Function = descriptor.value;
  if (original == null) return descriptor;

  descriptor.value = async function (...args: any[]) {
    console.log(`method ${key.toString()} starting to execute`);

    const start = performance.now();
    const result = await original.call(this, ...args);
    const end = performance.now();

    console.log(`method ${key.toString()} execution time: ${end - start}`);

    return result;
  };

  return descriptor;
};

const x: PropertyDecorator = (target, key) => {};
