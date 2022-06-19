import * as lib from "./lib.ts";
import { path, uuid } from "./deps.ts";
import testJson from "./test.json" assert { type: "json" };

console.log("%cDeno: hello, world", "color:purple; text-decoration:underline");

const fullName = lib.getPrefixedName("user");
console.log(`%cHello, ${fullName}`, "color:orange; background-color:blue;");

console.log(`UUID: ${uuid.v1.generate()}`);
console.log(`%c${JSON.stringify(testJson)}`, "color:green");

// alert("Please acknowledge the message.");
// console.log("The message has been acknowledged.");

// const shouldProceed = confirm("Do you want to proceed?");
// console.log("Should proceed?", shouldProceed);

// const name = prompt("Please enter your name:");
// console.log("Name:", name);

// const age = prompt("Please enter your age:", "18");
// console.log("Age:", age);

const PORT = Deno.env.get("PORT");
console.log("PORT:", PORT);

const env = Deno.env.toObject();
console.log("env:", env);
Deno.env.set("MY_PASSWORD", "123456");
Deno.env.delete("MY_PASSWORD");
Deno.env.set("MY_PASSWORD", "123");
Deno.env.set("my_password", "456");
console.log("UPPERCASE:", Deno.env.get("MY_PASSWORD"));
console.log("lowercase:", Deno.env.get("my_password"));
