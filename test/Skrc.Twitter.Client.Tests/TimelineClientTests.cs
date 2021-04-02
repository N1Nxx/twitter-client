using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Skrc.Twitter.Model;
using Xunit;

namespace Skrc.Twitter.Client.Tests
{
    public class AccessTokenClientTests
    {
        [Fact]
        public async Task Get_Access_Token_Should_Throw_Exception_When_Response_Not_Expected()
        {
            // Setup
            var mockResponse = JsonConvert.SerializeObject(new Exception("Invalid token"));
            var mockHanlder = new Mock<HttpMessageHandler>();
            mockHanlder.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    response.Content = new StringContent(mockResponse);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return response;
                });
            var client = new TwitterClient(new HttpClient(mockHanlder.Object), "http://baseurl", "", "");

            // Test

            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () => await client.GetAccessTokenAsync());
            Assert.Equal(mockResponse, exception.Message);
        }

        [Fact]
        public async Task Get_Access_Token_Should_Throw_Exception_When_Response_Invalid()
        {
            // Setup
            var mockResponse = JsonConvert.SerializeObject(new AccessTokenResponse
            {
                Type = "test"
            });
            var mockHanlder = new Mock<HttpMessageHandler>();
            mockHanlder.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    response.Content = new StringContent(mockResponse);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return response;
                });
            var client = new TwitterClient(new HttpClient(mockHanlder.Object), "http://baseurl", "", "");

            // Test

            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () => await client.GetAccessTokenAsync());
            Assert.Equal(mockResponse, exception.Message);
        }

        [Fact]
        public async Task Get_Access_Token_Should_Succeed()
        {
            // Setup
            var mockResponse = JsonConvert.SerializeObject(new AccessTokenResponse
            {
                Type = "type",
                AccessToken = "token"
            });
            var mockHanlder = new Mock<HttpMessageHandler>();
            mockHanlder.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    response.Content = new StringContent(mockResponse);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return response;
                });
            var client = new TwitterClient(new HttpClient(mockHanlder.Object), "http://baseurl", "", "");

            // Test
            var response = await client.GetAccessTokenAsync();

            // Assert
            Assert.NotNull(response);
            Assert.Equal("type", response.Type);
            Assert.Equal("token", response.AccessToken);
        }
    }
}
