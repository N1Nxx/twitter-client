using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Skrc.Twitter.Model;

namespace Skrc.Twitter.Client
{
    public partial class TwitterClient : ITwitterClient
    {
        public async Task<AccessTokenResponse> GetAccessTokenAsync()
        {
            // TODO: add cache
            AccessTokenResponse result = null;
            var formData = new List<KeyValuePair<string, string>>
            {
                { new KeyValuePair<string, string>("grant_type", "client_credentials") }
            };
            var request = CreateRequest(HttpMethod.Post,
                            $"{_baseUrl}oauth2/token",
                            new Dictionary<string, string>(),
                            string.Empty);
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{_apiKey}:{_apiSecretKey}")));
            request.Content = new FormUrlEncodedContent(formData);
            var response = await _client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<AccessTokenResponse>(responseContent);
            if (result == null)
            {
                throw new Exception(responseContent);
            }
            return result;
        }
    }
}
