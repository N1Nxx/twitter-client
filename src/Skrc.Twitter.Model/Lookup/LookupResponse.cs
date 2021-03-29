using Newtonsoft.Json;

namespace Skrc.Twitter.Model
{
    public class LookupResponse
    {
        [JsonProperty("data")]
        public LookupResponseUser Data { get; set; }
        public bool IsValid()
        {
            return Data != null &&
                !string.IsNullOrEmpty(Data.Id) &&
                !string.IsNullOrEmpty(Data.Name) &&
                !string.IsNullOrEmpty(Data.Username);
        }
    }
    public class LookupResponseUser
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
    }
}