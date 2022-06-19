import { parse } from "./deps.ts";

const name = Deno.args[0];
const food = Deno.args[1];

console.log(`Hello ${name}, I like ${food}!`);

const flags = parse(Deno.args, {
  boolean: ["help", "color"],
  string: ["version"],
  default: { color: true },
});

console.log("Wants help?", flags.help);
console.log("Version:", flags.version);
console.log("Wants color?:", flags.color);
console.log("Other:", flags._);
