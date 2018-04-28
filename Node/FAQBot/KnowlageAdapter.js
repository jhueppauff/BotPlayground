import http from "https";
import request from "request";

this.GetQnAAnswer = function(message, knowledgeBasesId, subscriptionKey, hostname) {
  var options = { method: 'POST',
    url: hostname + "/qnamaker/v1.0/knowledgebases/" + knowledgeBasesId + "/generateAnswer",
    headers: 
     { 'Cache-Control': 'no-cache',
       'Ocp-Apim-Subscription-Key': subscriptionKey,
       'Content-Type': 'application/json' },
    body: { question: 'when did world war 2 ended' },
    json: true };
  
  request(options, function (error, response, body) {
    if (error) throw new Error(error);
  
    return(body);
  });
}