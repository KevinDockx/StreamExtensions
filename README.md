# Stream Extensions
A set of helper extension methods for working with streams.  Particularly useful for interaction with an API through HttpClient.  Includes 24 extension methods in total (sync & async where it makes sense, generic & non-generic) like `SerializeToJsonAndWriteAsync<T>`, `SerializeToJsonAndWrite` and `ReadAndDeserializeFromJson<T>`.


# Installation (NuGet)
```
Install-Package Marvin.StreamExtensions
```

# Usage 

Add a using statement to import the extension methods
```
using Marvin.StreamExtensions
```

From this moment on any Stream object will have a set of additional extension methods available.  

To deserialize the response of an HttpRequest from Json, using streams:  

```
using (var response = await httpClient.SendAsync(request))
{
    var stream = await response.Content.ReadAsStreamAsync();
    var person = stream.ReadAndDeserializeFromJson<Person>();
}
```

To use a stream for sending data: 

```
var person = new Person() { Name = "Lord Flashheart" };
var memoryContentStream = new MemoryStream();
memoryContentStream.SerializeToJsonAndWrite<Person>(person);

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
                // handle the response
            }
        }
    }
```
