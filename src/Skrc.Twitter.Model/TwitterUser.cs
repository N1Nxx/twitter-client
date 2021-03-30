using System.Collections.Generic;
using Newtonsoft.Json;

namespace Skrc.Twitter.Model
{
    public class TwitterUser
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }

        public List<TimelineTweet> Tweets { get; set; }
    }
}