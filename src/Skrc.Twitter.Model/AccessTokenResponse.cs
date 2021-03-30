using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Skrc.Twitter.Model
{
    [ExcludeFromCodeCoverage]
    public class AccessTokenResponse
    {
        [JsonProperty("token_type")]
        public string Type { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
