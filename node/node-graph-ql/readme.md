# Readme

Run server with `npm run dev` and call `localhost:3000/graphql` endpoint.

Request example:

```graphql
{
  quoteOfTheDay
  random
  rollThreeDice
  rollDice(numDice: 2, numSides: 6)
  getDie(numSides: 6) {
    rollOnce
    roll(numRolls: 3)
  }
}
```

Client call:

```js
var dice = 3;
var sides = 6;
var query = `query RollDice($dice: Int!, $sides: Int) {
  rollDice(numDice: $dice, numSides: $sides)
}`;

fetch("/graphql", {
  method: "POST",
  headers: {
    "Content-Type": "application/json",
    Accept: "application/json",
  },
  body: JSON.stringify({
    query,
    variables: { dice, sides },
  }),
})
  .then((r) => r.json())
  .then((data) => console.log("data returned:", data));
```

To create and get message:

```graphql
query {
  getMessage(id: "<message-id>") {
    id
    content
    author
  }
}

mutation {
  createMessage(input: { author: "andy", content: "hope is a good thing" }) {
    id
  }
}
```

To update message:

```graphql
mutation {
  updateMessage(
    id: "1d6a7b46ab59d46ab026"
    input: { author: "andy", content: "updated" }
  ) {
    id
  }
}
```
