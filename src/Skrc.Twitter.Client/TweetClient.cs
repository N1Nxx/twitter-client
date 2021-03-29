using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Skrc.Twitter.Model;

namespace Skrc.Twitter.Client
{
    public partial class TwitterClient : ITwitterClient
    {
        public async Task<TimelineResponse> GetUserTimelineAsync(string token, string userId)
        {
            TimelineResponse result = null;
            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(userId))
            {
                var request = CreateRequest(HttpMethod.Get,
                                $"{_baseUrl}2/users/{userId}/tweets",
                                new Dictionary<string, string>
                                {
                                    { "Authorization", $"Bearer {token}" }
                                },
                                string.Empty);
                var response = await _client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<TimelineResponse>(responseContent);
                if (result == null)
                {
                    throw new Exception(responseContent);
                }
            }
            return result;
        }
    }
}