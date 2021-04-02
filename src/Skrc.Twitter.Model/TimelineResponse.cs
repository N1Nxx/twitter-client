using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Skrc.Twitter.Model
{
    public class TimelineResponse
    {
        [JsonProperty("data")]
        public List<TimelineTweet> Data { get; set; }

        [JsonProperty("meta")]
        public TimelineMetadata Meta { get; set; }
        public bool IsValid()
        {
            return Data != null && Meta != null;
        }
    }

    [ExcludeFromCodeCoverage]
    public class TimelineMetadata
    {
        [JsonProperty("newest_id")]
        public string NewestId { get; set; }
        [JsonProperty("oldest_id")]
        public string OldestId { get; set; }
        [JsonProperty("next_token")]
        public string NextToken { get; set; }
        [JsonProperty("result_count")]
        public int Count { get; set; }
    }
}
