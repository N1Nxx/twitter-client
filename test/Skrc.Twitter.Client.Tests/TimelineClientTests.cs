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
    public class TimelineClientTests
    {
        [Fact]
        public async Task GetUserTimeline_Should_Throw_Exception_When_Response_Not_Expected()
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
            var exception = await Assert.ThrowsAsync<Exception>(async () => await client.GetUserTimelineAsync("token", "userId"));
            Assert.Equal(mockResponse, exception.Message);
        }

        [Fact]
        public async Task GetUserTimeline_Should_Throw_Exception_When_Response_Invalid()
        {
            // Setup
            var mockResponse = JsonConvert.SerializeObject(new TimelineResponse
            {
                Data = new List<TimelineTweet>()
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
            var exception = await Assert.ThrowsAsync<Exception>(async () => await client.GetUserTimelineAsync("token", "userId"));
            Assert.Equal(mockResponse, exception.Message);
        }

        [Fact]
        public async Task GetUserTimeline_Should_Fail()
        {
            // Setup
            var client = new TwitterClient(new HttpClient(), "http://baseurl", "", "");

            // Test
            var response = await client.GetUserTimelineAsync("", "");
            var responseNull = await client.GetUserTimelineAsync(null, null);
            var responseUserId = await client.GetUserTimelineAsync("token", "");
            var responseUserIdNull = await client.GetUserTimelineAsync("token", null);
            var responseToken = await client.GetUserTimelineAsync("", "userId");
            var responseTokenNull = await client.GetUserTimelineAsync(null, "userId");

            // Assert
            Assert.Null(response);
            Assert.Null(responseNull);
            Assert.Null(responseUserId);
            Assert.Null(responseUserIdNull);
            Assert.Null(responseToken);
            Assert.Null(responseTokenNull);
        }

        [Fact]
        public async Task GetUserTimeline_Should_Succeed()
        {
            // Setup
            var mockResponse = JsonConvert.SerializeObject(new TimelineResponse
            {
                Data = new List<TimelineTweet>(),
                Meta = new TimelineMetadata
                {
                    Count = 1
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
            var response = await client.GetUserTimelineAsync("token", "userId");

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.NotNull(response.Meta);
            Assert.Equal(1, response.Meta.Count);
        }
    }
}
