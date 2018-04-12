using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;

namespace FAQBot.Dialogs
{
    public class DialogBuilder
    {
        private IDialogContext context;

        public DialogBuilder(IDialogContext context)
        {
            this.context = context;
        }

        public string MessageRoute(string message)
        {
            char[] punctuation = { '!', '?', '.', ',' };

            message = message.TrimEnd(punctuation);

            // Needs to be replaced with intends
            switch (message.ToLowerInvariant())
            {
                case "hi":
                    return Greetings();
                case "who are you":
                    return WhoAmI();
                case "what are you":
                    return WhoAmI();
                case "what time is it":
                    return $"It is :{DateTime.UtcNow.ToString()} (UTC)";
                case "what date is today":
                    return GetDate();
                default:
                    return SolveQuestion();
            }
        }

        public string Greetings()
        {
            return "Hi, Please ask me any question. I will try to anser them.";
        }

        public string WhoAmI()
        {
            string hash = string.Empty;

            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(Assembly.GetExecutingAssembly().Location))
                {
                    var md5Hash = md5.ComputeHash(stream);
                    hash = BitConverter.ToString(md5Hash).Replace("-", "").ToLowerInvariant();
                }
            }

                return $"I am a bot. I do not have a Name. I have a hash value {hash}. Do you have a hash too?";
        }

        public string SolveQuestion()
        {
            // DB Connection

            if (true)
            {

            }

            return "Sorry, I didn't understand this question. Please try again.";
        }

        public string GetDate()
        {
            return $"Today we have the {DateTime.Now.Date}";
        }
    }
}