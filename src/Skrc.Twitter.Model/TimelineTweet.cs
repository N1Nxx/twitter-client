using System;
using Newtonsoft.Json;

namespace Skrc.Twitter.Model
{
    public class TimelineTweet
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}