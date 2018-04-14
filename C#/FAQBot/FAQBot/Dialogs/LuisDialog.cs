namespace FAQBot.Dialogs
{
    using System;
    using System.Configuration;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.Luis;
    using Microsoft.Bot.Builder.Luis.Models;

    [Serializable]
    public class LuisDialog : LuisDialog<object>
    {
        public LuisDialog() : base(new LuisService(new LuisModelAttribute(
           ConfigurationManager.AppSettings["LuisAppId"],
           ConfigurationManager.AppSettings["LuisApiKey"],
           domain: ConfigurationManager.AppSettings["LuisApiHostName"])))
        {
        }

        // Go to https://luis.ai and create a new intent, then train/publish your luis app.
        // Finally replace "Greeting" with the name of your newly created intent in the following handler
        [LuisIntent("Greetings")]
        public async Task GreetingIntent(IDialogContext context, LuisResult result)
        {
            string message = $"Hi. How may I help you today?";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry, I did not understand you.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("CurrentDate")]
        public async Task CurrentDate(IDialogContext context, LuisResult result)
        {
            string message = $"Today we have the {DateTime.Today.Date.ToString("dd.MM.yyyy")}";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("CurrentTime")]
        public async Task CurrentTime(IDialogContext context, LuisResult result)
        {
            string message = $"The current time is {DateTime.UtcNow.ToString("hh:mm:ss")} UTC";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Question")]
        public async Task AskQuestion(IDialogContext context, LuisResult result)
        {
            RestClient restClient = new RestClient();
            string jsonResult = restClient.GetKB(result.Query);

            KBResult kBResult = new KBResult();

            try
            {
                kBResult = Newtonsoft.Json.JsonConvert.DeserializeObject<KBResult>(jsonResult);
            }
            catch (Exception)
            {

                throw new Exception("Unable to deserialize response string");
            }

            await context.PostAsync(kBResult.Answer);

            context.Wait(this.MessageReceived);
        }
    }
}