using Newtonsoft.Json;

namespace Skrc.Twitter.Model
{
    public class LookupResponse
    {
        [JsonProperty("data")]
        public TwitterUser Data { get; set; }
        public bool IsValid()
        {
            return Data != null &&
                !string.IsNullOrEmpty(Data.Id) &&
                !string.IsNullOrEmpty(Data.Name) &&
                !string.IsNullOrEmpty(Data.Username);
        }
    }
}