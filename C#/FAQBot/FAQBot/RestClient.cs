namespace FAQBot
{
    using System.Configuration;

    public class RestClient
    {
        public string GetKB(string question)
        {
            string qnamakerUriBase = $"https://{ConfigurationManager.AppSettings["QnAApiHostName"]}/qnamaker/v1.0";
            string apiApppId = ConfigurationManager.AppSettings["QnAKnowledgebaseId"];
            string apiKey = ConfigurationManager.AppSettings["QnASubscriptionKey"];

            string body = $"{{\"question\" : \"{question}\"}}";

            qnamakerUriBase += $"/knowledgebases/{apiApppId}/generateAnswer";

            RestSharp.RestClient client = new RestSharp.RestClient(qnamakerUriBase)
            {
                Encoding = System.Text.Encoding.UTF8,
            };

            RestSharp.RestRequest request = new RestSharp.RestRequest(RestSharp.Method.POST);

            // Add Header
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Ocp-Apim-Subscription-Key", apiKey);
            request.AddHeader("cache-control", "no-cache");

            // Add Body
            request.AddParameter("undefined", body, RestSharp.ParameterType.RequestBody);

            // Send the Post Request
            RestSharp.IRestResponse response = client.Execute(request);

            return response.Content;
        }
    }
}