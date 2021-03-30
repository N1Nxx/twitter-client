using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Skrc.Twitter.Model
{
    [ExcludeFromCodeCoverage]
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