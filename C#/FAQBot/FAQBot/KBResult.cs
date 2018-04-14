using Newtonsoft.Json;

namespace FAQBot
{
    public class KBResult
    {
        [JsonProperty(PropertyName = "answer")]
        public string Answer { get; set; }

        [JsonProperty(PropertyName = "score")]
        public double Score { get; set; }

    }
}