var http = require("https");
var request = require("request");

module.exports = {

  GetQnAAnswer : function (message, knowledgeBasesId, subscriptionKey, hostname) {
    return new Promise(function (resolve) {
    var options = { method: 'POST',
      url: "https://" +  hostname + "/qnamaker/v1.0/knowledgebases/" + knowledgeBasesId + "/generateAnswer",
      headers: 
      { 'Cache-Control': 'no-cache',
        'Ocp-Apim-Subscription-Key': subscriptionKey,
        'Content-Type': 'application/json' },
      body: { question: message },
      json: true };
    
      request(options, function (error, response, body) {
      if (error) throw new Error(error);
    
      resolve(body.answer);
    });
    });
  }
};