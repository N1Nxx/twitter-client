using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Skrc.Twitter.Model
{
    public class AccessTokenResponse
    {
        [JsonProperty("token_type")]
        public string Type { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(AccessToken);
        }
    }
}
