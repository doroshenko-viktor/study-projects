var express = require('express');
var { graphqlHTTP } = require('express-graphql');
var { buildSchema } = require('graphql');

var schema = buildSchema(`
  input MessageInput {
    content: String
    author: String
  }

  type Message {
    id: ID!
    content: String
    author: String
  }

  type RandomDie {
    numSides: Int!
    rollOnce: Int!
    roll(numRolls: Int!): [Int]
  }

  type Query {
    quoteOfTheDay: String
    random: Float!
    rollThreeDice: [Int],
    rollDice(numDice: Int!, numSides: Int): [Int],
    getDie(numSides: Int): RandomDie,
    getMessage(id: ID!): Message
  }

  type Mutation {
    createMessage(input: MessageInput): Message
    updateMessage(id: ID!, input: MessageInput): Message
  }
`);

class Message {
    constructor(id, { content, author }) {
        this.id = id;
        this.content = content;
        this.author = author;
    }
}

var DB = {};

class RandomDie {
    constructor(numSides) {
        this.numSides = numSides;
    }

    rollOnce() {
        return 1 + Math.floor(Math.random() * this.numSides);
    }

    roll({ numRolls }) {
        var output = [];
        for (var i = 0; i < numRolls; i++) {
            output.push(this.rollOnce());
        }
        return output;
    }
}

var root = {
    quoteOfTheDay: (args, req) => {
        console.log(req);
        return Math.random() < 0.5 ? 'Take it easy' : 'Salvation lies within';
    },
    random: () => {
        return Math.random();
    },
    rollThreeDice: () => {
        return [1, 2, 3].map(_ => 1 + Math.floor(Math.random() * 6));
    },
    rollDice: ({ numDice, numSides }) => {
        var output = [];
        for (var i = 0; i < numDice; i++) {
            output.push(1 + Math.floor(Math.random() * (numSides || 6)));
        }
        return output;
    },
    getDie: ({ numSides }) => {
        return new RandomDie(numSides || 6);
    },
    getMessage: ({ id }) => {
        if (!DB[id]) {
            throw new Error(`no message exists with id ${id}`);
        }
        return new Message(id, DB[id]);
    },
    createMessage: ({ input }) => {
        var id = require('crypto').randomBytes(10).toString('hex');

        DB[id] = input;
        return new Message(id, input);
    },
    updateMessage: ({ id, input }) => {
        if (!DB[id]) {
            throw new Error('no message exists with id ' + id);
        }
        DB[id] = input;
        return new Message(id, input);
    },
};

var app = express();
app.use('/graphql', graphqlHTTP({
    schema: schema,
    rootValue: root,
    graphiql: true,
}));

const port = 3000;
app.listen(port, () => console.log(`listening on port: ${port}`));