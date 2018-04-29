var configuration = require("./config.json");
var restify = require('restify');
var builder = require('botbuilder');
var KnowlageAdapter = require('./KnowlageAdapter.js');

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

var inMemoryStorage = new builder.MemoryBotStorage();

// Receive messages from the user and respond by echoing each message back (prefixed with 'You said:')
var bot = new builder.UniversalBot(connector, function (session) {
    session.send('Sorry, I did not understand \'%s\'. Type \'help\' if you need assistance.', session.message.text);
}).set('storage', inMemoryStorage);

    var recognizer = new builder.LuisRecognizer(configuration.LuisUrl);
    bot.recognizer(recognizer);

    bot.dialog("Greetings", function(session, args) {
        session.send("Hi. How may I help you today?");
    }).triggerAction({ matches: "Greetings"});

    bot.dialog("CurrentTime", function(session, args) {
        session.send("It is " + getDateTime());
    }).triggerAction({
        matches: 'CurrentTime'
    });

    bot.dialog("Question", function(session, args) {
        KnowlageAdapter.GetQnAAnswer(session.message.text, configuration.QnAKnowledgebaseId, configuration.QnASubscriptionKey, configuration.QnAApiHostName).then(
            function (answer){
                session.send(answer)
            }
        );
        
    }).triggerAction({
        matches: 'Question'
    });
      
    function getDateTime() {
        var date = new Date();
        var hour = date.getHours();
        hour = (hour < 10 ? "0" : "") + hour;
        var min  = date.getMinutes();
        min = (min < 10 ? "0" : "") + min;

        return hour + ":" + min;
    }