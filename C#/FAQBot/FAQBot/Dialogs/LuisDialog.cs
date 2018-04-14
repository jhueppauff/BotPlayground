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

            await PostMessageAsync(context, message);
        }

        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry, I did not understand you.";

            await PostMessageAsync(context, message);
        }

        [LuisIntent("CurrentDate")]
        public async Task CurrentDate(IDialogContext context, LuisResult result)
        {
            string message = $"Today we have the {DateTime.Now.Date}";

            await PostMessageAsync(context, message);
        }

        [LuisIntent("CurrentTime")]
        public async Task CurrentTime(IDialogContext context, LuisResult result)
        {
            string message = $"The current Time is {DateTime.UtcNow}";

            await PostMessageAsync(context, message);
        }

        private async Task PostMessageAsync(IDialogContext context, string message)
        {
            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }
    }
}