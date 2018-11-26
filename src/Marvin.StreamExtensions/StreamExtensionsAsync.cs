using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Marvin.StreamExtensions
{
    /// <summary>
    /// Extensions for working with streams
    /// </summary>
    public static partial class StreamExtensions
    {
        // note: there aren't async versions of ReadAndDeserialize as Json.NET currently doesn't support that.

        public static async Task SerializeAndWriteToJsonAsync<T>(this Stream stream, T objectToWrite)
        {
            await SerializeAndWriteToJsonAsync<T>(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, false);
        }

        public static async Task SerializeAndWriteToJsonAsync<T>(this Stream stream, T objectToWrite,
          Encoding encoding)
        {
            await SerializeAndWriteToJsonAsync<T>(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, false);
        }

        public static async Task SerializeAndWriteToJsonAsync<T>(this Stream stream, T objectToWrite,
         Encoding encoding, int bufferSize)
        {
            await SerializeAndWriteToJsonAsync<T>(stream, objectToWrite, encoding, bufferSize, false, false);
        }

        public static async Task SerializeAndWriteToJsonAsync<T>(this Stream stream, T objectToWrite,
        Encoding encoding, int bufferSize, bool leaveOpen)
        {
            await SerializeAndWriteToJsonAsync<T>(stream, objectToWrite, encoding, bufferSize, leaveOpen, false);
        }

        public static async Task SerializeAndWriteToJsonAsync<T>(this Stream stream, T objectToWrite, bool resetStream)
        {
            await SerializeAndWriteToJsonAsync<T>(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, resetStream);
        }

        public static async Task SerializeAndWriteToJsonAsync<T>(this Stream stream, T objectToWrite,
      Encoding encoding, bool resetStream)
        {
            await SerializeAndWriteToJsonAsync<T>(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, resetStream);
        }
        
        public static async Task SerializeAndWriteToJsonAsync<T>(this Stream stream, T objectToWrite,
          Encoding encoding,
          int bufferSize,
          bool leaveOpen,
          bool resetStream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanWrite)
            {
                throw new NotSupportedException("Can't write to this stream.");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            using (var streamWriter = new StreamWriter(stream, encoding, bufferSize, leaveOpen))
            {
                using (var jsonTextWriter = new JsonTextWriter(streamWriter))
                {
                    var jsonSerializer = new JsonSerializer();
                    jsonSerializer.Serialize(jsonTextWriter, objectToWrite);
                    await jsonTextWriter.FlushAsync();
                }
            }

            // after writing, set the stream to position 0
            if (resetStream & stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }
        }

        public static async Task SerializeAndWriteToJsonAsync(this Stream stream, object objectToWrite)
        {
            await SerializeAndWriteToJsonAsync(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, false);
        }

        public static async Task SerializeAndWriteToJsonAsync(this Stream stream, object objectToWrite,
          Encoding encoding)
        {
            await SerializeAndWriteToJsonAsync(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, false);
        }

        public static async Task SerializeAndWriteToJsonAsync(this Stream stream, object objectToWrite,
         Encoding encoding, int bufferSize)
        {
            await SerializeAndWriteToJsonAsync(stream, objectToWrite, encoding, bufferSize, false, false);
        }

        public static async Task SerializeAndWriteToJsonAsync(this Stream stream, object objectToWrite,
        Encoding encoding, int bufferSize, bool leaveOpen)
        {
            await SerializeAndWriteToJsonAsync(stream, objectToWrite, encoding, bufferSize, leaveOpen, false);
        }

        public static async Task SerializeAndWriteToJsonAsync(this Stream stream, object objectToWrite, bool resetStream)
        {
            await SerializeAndWriteToJsonAsync(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, resetStream);
        }

        public static async Task SerializeAndWriteToJsonAsync(this Stream stream, object objectToWrite,
        Encoding encoding, bool resetStream)
        {
            await SerializeAndWriteToJsonAsync(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, resetStream);
        }

        public static async Task SerializeAndWriteToJsonAsync(this Stream stream, object objectToWrite,
       Encoding encoding,
       int bufferSize,
       bool leaveOpen,
       bool resetStream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanWrite)
            {
                throw new NotSupportedException("Can't write to this stream.");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            using (var streamWriter = new StreamWriter(stream, encoding, bufferSize, leaveOpen))
            {
                using (var jsonTextWriter = new JsonTextWriter(streamWriter))
                {
                    var jsonSerializer = new JsonSerializer();
                    jsonSerializer.Serialize(jsonTextWriter, objectToWrite);
                    await jsonTextWriter.FlushAsync();
                }
            }

            // after writing, set the stream to position 0
            if (resetStream & stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }
        }
    }
}
