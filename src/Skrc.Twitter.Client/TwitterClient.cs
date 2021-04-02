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
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        private readonly string _apiKey;
        private readonly string _apiSecretKey;

        public TwitterClient(HttpClient client, string baseUrl, string apiKey, string apiSecretKey)
        {
            _client = client;
            _baseUrl = baseUrl;
            _apiKey = apiKey;
            _apiSecretKey = apiSecretKey;
        }

        protected HttpRequestMessage CreateRequest(HttpMethod method, string url,
                Dictionary<string, string> customHeaders, string body)
        {
            var request = new HttpRequestMessage(method, url);
            request.Headers.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            foreach (var key in customHeaders.Keys)
            {
                request.Headers.Add(key, customHeaders[key]);
            }
            request.Content = new StringContent(body, Encoding.UTF8, "application/json");
            return request;
        }
    }
}
