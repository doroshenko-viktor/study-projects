type ObjectKey<T> = keyof T;

export function overwrite<T extends Object>(
  target: T,
  key: string | symbol
): void {
  const objectKey = key as ObjectKey<typeof target>;
  const field = target[objectKey];

  Object.defineProperty(target, objectKey, {
    get: () => "overwritten",
    set: () => {},
    enumerable: false,
    configurable: false,
  });
}
