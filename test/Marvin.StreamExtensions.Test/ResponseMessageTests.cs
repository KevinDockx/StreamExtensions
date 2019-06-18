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
	public class ResponseMessageTests
	{
		[Fact]
		public async Task DeserializeTypedResponse_MustMatchInput()
		{
			var person = new Person { Name = "Lord Flashheart" };
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
				personAfterResponse = await response.DeserializeAsStreamAsync<Person>(new UTF8Encoding(), false, 1024, true);
			}

			Assert.Equal(person, personAfterResponse);
		}

		[Fact]
		public async Task DeserializeResponse_MustMatchInput()
		{
			var person = new Person { Name = "Lord Flashheart" };
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
				var output = await response.DeserializeAsStreamAsync<object>(new UTF8Encoding(), false, 1024, true);
				// cast - just testing the non-typed ReadAndDeserializeFromJson method
				personAfterResponse = ((JObject)output).ToObject<Person>();
			}
			Assert.Equal(person, personAfterResponse);
		}
	}
}