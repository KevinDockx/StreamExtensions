using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Marvin.StreamExtensions.Test
{
    public class StreamTests
    {
        [Fact]
        public async Task SerializeInputToStream_MustMatchInput()
        {
            var person = new Person() { Name = "Lord Flashheart" };
            Person personAfterResponse;

            // create mocked HttpMessageHandler 
            var bounceInputHttpMessageHandlerMock = new Mock<HttpMessageHandler>();

            // set up the method
            bounceInputHttpMessageHandlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(JsonConvert.SerializeObject(person))
               });

            // instantiate client
            var httpClient = new HttpClient(bounceInputHttpMessageHandlerMock.Object);

            var memoryContentStream = new MemoryStream();
            memoryContentStream.SerializeToJsonAndWrite(person, new UTF8Encoding(), 1024, true, true); 
             
            using (var request = new HttpRequestMessage(
                HttpMethod.Post,
                "http://api/test"))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (var streamContent = new StreamContent(memoryContentStream))
                {
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    request.Content = streamContent;

                    using (var response = await httpClient.SendAsync(request))
                    {
                        _ = await response.Content.ReadAsStreamAsync();

                        response.EnsureSuccessStatusCode();                        
                        personAfterResponse = JsonConvert.DeserializeObject<Person>(await response.Content.ReadAsStringAsync());                         
                    }
                }
            } 
             
            Assert.Equal(person, personAfterResponse);
        }

        [Fact]
        public async Task SerializeInputToStream_Async_MustMatchInput()
        {
            var person = new Person() { Name = "Lord Flashheart" };
            Person personAfterResponse;

            // create mocked HttpMessageHandler 
            var bounceInputHttpMessageHandlerMock = new Mock<HttpMessageHandler>();

            // set up the method
            bounceInputHttpMessageHandlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(JsonConvert.SerializeObject(person))
               });

            // instantiate client
            var httpClient = new HttpClient(bounceInputHttpMessageHandlerMock.Object);

            var memoryContentStream = new MemoryStream();
            await memoryContentStream.SerializeToJsonAndWriteAsync(person, new UTF8Encoding(), 1024, false, true);

            using (var request = new HttpRequestMessage(
                HttpMethod.Post,
                "http://api/test"))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (var streamContent = new StreamContent(memoryContentStream))
                {
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    request.Content = streamContent;

                    using (var response = await httpClient.SendAsync(request))
                    {
                        _ = await response.Content.ReadAsStreamAsync();

                        response.EnsureSuccessStatusCode();
                        personAfterResponse = JsonConvert.DeserializeObject<Person>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            Assert.Equal(person, personAfterResponse);
        }
        
        [Fact]
        public async Task SerializeTypedInputToStream_MustMatchInput()
        {
            var person = new Person() { Name = "Lord Flashheart" };
            Person personAfterResponse;

            // create mocked HttpMessageHandler 
            var bounceInputHttpMessageHandlerMock = new Mock<HttpMessageHandler>();

            // set up the method
            bounceInputHttpMessageHandlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(JsonConvert.SerializeObject(person))
               });

            // instantiate client
            var httpClient = new HttpClient(bounceInputHttpMessageHandlerMock.Object); 

            var memoryContentStream = new MemoryStream();
            memoryContentStream.SerializeToJsonAndWrite(person, new UTF8Encoding(), 1024, true, true);

            using (var request = new HttpRequestMessage(
                HttpMethod.Post,
                "http://api/test"))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (var streamContent = new StreamContent(memoryContentStream))
                {
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    request.Content = streamContent;

                    using (var response = await httpClient.SendAsync(request))
                    {
                        _ = await response.Content.ReadAsStreamAsync();

                        response.EnsureSuccessStatusCode();
                        personAfterResponse = JsonConvert.DeserializeObject<Person>(
                            await response.Content.ReadAsStringAsync());
                    }
                }
            }
            Assert.Equal(person, personAfterResponse);
        }

        [Fact]
        public async Task SerializeTypedInputToStream_Async_MustMatchInput()
        {
            var person = new Person() { Name = "Lord Flashheart" };
            Person personAfterResponse;

            // create mocked HttpMessageHandler 
            var bounceInputHttpMessageHandlerMock = new Mock<HttpMessageHandler>();

            // set up the method
            bounceInputHttpMessageHandlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(JsonConvert.SerializeObject(person))
               });

            // instantiate client
            var httpClient = new HttpClient(bounceInputHttpMessageHandlerMock.Object);

            var memoryContentStream = new MemoryStream();
            await memoryContentStream.SerializeToJsonAndWriteAsync(person, new UTF8Encoding(), 1024, true, true);

            using (var request = new HttpRequestMessage(
                HttpMethod.Post,
                "http://api/test"))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (var streamContent = new StreamContent(memoryContentStream))
                {
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    request.Content = streamContent;

                    using (var response = await httpClient.SendAsync(request))
                    {
                        _ = await response.Content.ReadAsStreamAsync();

                        response.EnsureSuccessStatusCode();
                        personAfterResponse = JsonConvert.DeserializeObject<Person>(
                            await response.Content.ReadAsStringAsync());
                    }
                }
            }
            Assert.Equal(person, personAfterResponse);
        }


        [Fact]
        public async Task DeserializeTypedResponseFromStream_MustMatchInput()
        {
            var person = new Person() { Name = "Lord Flashheart" };
            Person personAfterResponse;

            // create mocked HttpMessageHandler 
            var bounceInputHttpMessageHandlerMock = new Mock<HttpMessageHandler>();

            // set up the method
            bounceInputHttpMessageHandlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(JsonConvert.SerializeObject(person))
               });

            // instantiate client
            var httpClient = new HttpClient(bounceInputHttpMessageHandlerMock.Object);

            // send some json
            var request = new HttpRequestMessage(HttpMethod.Post, "http://api/test")
            {
                Content = new StringContent(JsonConvert.SerializeObject(person))
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.SendAsync(request))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                personAfterResponse = stream.ReadAndDeserializeFromJson<Person>(new UTF8Encoding(), false, 1024, true);
            }

            Assert.Equal(person, personAfterResponse);
        }
        
        [Fact]
        public async Task DeserializeResponseFromStream_MustMatchInput()
        {
            var person = new Person() { Name = "Lord Flashheart" };
            Person personAfterResponse;

            // create mocked HttpMessageHandler 
            var bounceInputHttpMessageHandlerMock = new Mock<HttpMessageHandler>();

            // set up the method
            bounceInputHttpMessageHandlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(JsonConvert.SerializeObject(person))
               });

            // instantiate client
            var httpClient = new HttpClient(bounceInputHttpMessageHandlerMock.Object);

            // send some json
            var request = new HttpRequestMessage(HttpMethod.Post, "http://api/test")
            {
                Content = new StringContent(JsonConvert.SerializeObject(person))
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (var response = await httpClient.SendAsync(request))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                var output = stream.ReadAndDeserializeFromJson(new UTF8Encoding(), false, 1024, true);
                // cast - just testing the non-typed ReadAndDeserializeFromJson method
                personAfterResponse = ((JObject)output).ToObject<Person>();
            }
            Assert.Equal(person, personAfterResponse);
        }

		[Fact]
		public async Task SerializeTypedInputToStream_Async_CanceledThrowsTaskCanceledException()
		{
			var person = new Person();
			var cancellationTokenSource = new CancellationTokenSource();
			cancellationTokenSource.Cancel();

			using (var memoryStream = new MemoryStream())
			{
				await Assert.ThrowsAsync<TaskCanceledException>(async () =>
					// test simple overload to also ensure the more complex overload is passed the token
					await memoryStream.SerializeToJsonAndWriteAsync(person, cancellationTokenSource.Token)
				);
			}
		}
	}
}
