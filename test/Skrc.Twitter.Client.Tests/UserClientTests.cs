using System;
using System.Collections.Generic;
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
    public class UserClientTests
    {
        [Fact]
        public async Task LookupUserAsync_Should_Throw_Exception_When_Response_Not_Expected()
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
            var exception = await Assert.ThrowsAsync<Exception>(async () => await client.LookupUserAsync("token", "username"));
            Assert.Equal(mockResponse, exception.Message);
        }

        [Fact]
        public async Task LookupUserAsync_Should_Throw_Exception_When_Response_Invalid()
        {
            // Setup
            var mockResponse = JsonConvert.SerializeObject(new LookupResponse
            {
                Data = new TwitterUser
                {
                    Id = "1234"
                }
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
            var exception = await Assert.ThrowsAsync<Exception>(async () => await client.LookupUserAsync("token", "username"));
            Assert.Equal(mockResponse, exception.Message);
        }

        [Fact]
        public async Task LookupUserAsync_Should_Fail()
        {
            // Setup
            var client = new TwitterClient(new HttpClient(), "http://baseurl", "", "");

            // Test
            var response = await client.LookupUserAsync("", "");
            var responseNull = await client.LookupUserAsync(null, null);
            var responseUsername = await client.LookupUserAsync("token", "");
            var responseUsernameNull = await client.LookupUserAsync("token", null);
            var responseToken = await client.LookupUserAsync("", "userId");
            var responseTokenNull = await client.LookupUserAsync(null, "userId");

            // Assert
            Assert.Null(response);
            Assert.Null(responseNull);
            Assert.Null(responseUsername);
            Assert.Null(responseUsernameNull);
            Assert.Null(responseToken);
            Assert.Null(responseTokenNull);
        }

        [Fact]
        public async Task LookupUserAsync_Should_Succeed()
        {
            // Setup
            var mockResponse = JsonConvert.SerializeObject(new LookupResponse
            {
                Data = new TwitterUser
                {
                    Id = "1",
                    Name = "Name",
                    Username = "Username"
                }
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
            var response = await client.LookupUserAsync("token", "username");

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal("1", response.Data.Id);
        }
    }
}
