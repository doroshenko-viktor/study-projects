/** @type {import('ts-jest/dist/types').InitialOptionsTsJest} */
module.exports = {
  preset: 'ts-jest',
  testEnvironment: 'node',
  moduleNameMapper: {
    "@exmpl/(.*)": "<rootDir>/src/$1"
  },
  "testRegex": "((/__tests__/).*|(\\.|/)(test|spec))\\.(ts|js)$",
  "coverageDirectory": "coverage",
  "collectCoverageFrom": [
    "src/**/*.{ts}",
    "!src/**/*.d.ts"
  ]
};