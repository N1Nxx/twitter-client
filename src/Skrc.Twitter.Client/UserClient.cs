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
        public async Task<LookupResponse> LookupUserAsync(string token, string username)
        {
            LookupResponse result = null;
            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(username))
            {
                var request = CreateRequest(HttpMethod.Get,
                                $"{_baseUrl}2/users/by/username/{username}",
                                new Dictionary<string, string>
                                {
                                    { "Authorization", $"Bearer {token}" }
                                },
                                string.Empty);
                var response = await _client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<LookupResponse>(responseContent);
                if (result == null)
                {
                    throw new Exception(responseContent);
                }
            }
            return result;
        }
    }
}