export function readonly(constructor: Function) {
  Object.freeze(constructor);
  Object.freeze(constructor.prototype);
}

export const entity = <T extends { new (...args: any[]): {} }>(target: T) => {
  return class extends target {
    public id = Math.random();
    public createDate = Date.now();
  };
};
