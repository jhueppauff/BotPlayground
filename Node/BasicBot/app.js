
var restify = require('restify');
var builder = require('botbuilder');

// Setup Restify Server
var server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () {
   console.log('%s listening to %s', server.name, server.url); 
});

// Create chat connector for communicating with the Bot Framework Service
var connector = new builder.ChatConnector({
    appId: process.env.MicrosoftAppId,
    appPassword: process.env.MicrosoftAppPassword
});

// Listen for messages from users 
server.post('/api/messages', connector.listen());

// Receive messages from the user and respond by echoing each message back (prefixed with 'You said:')
var bot = new builder.UniversalBot(connector, function (session) {

    // Validate user input
    switch(session.message.text)
    {
        case "hi":
            session.send("Hello Master!");
            break;
        case "Hi":
            session.send("Hello Master!");
            break;
        case "selfdestruction":
            session.send("Nope, I wont kill myself!");
            break;
        default:
            session.send("Unknown command.");
            break;
    }
    
});