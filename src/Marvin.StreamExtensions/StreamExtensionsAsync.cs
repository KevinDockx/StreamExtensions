using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Marvin.StreamExtensions
{
    /// <summary>
    /// Extensions for working with streams
    /// </summary>
    public static partial class StreamExtensions
    {
        /// <summary>
        /// Read from the stream and deserialize into an object of type T (assuming Json content).
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="stream">The stream</param>
        /// <returns>An object of type T</returns>
        public static async Task<T> ReadAndDeserializeFromJsonAsync<T>(
            this Stream stream)
        {
            return await ReadAndDeserializeFromJsonAsync<T>(stream, new UTF8Encoding(), true,
                Defaults.DefaultBufferSizeOnRead, false);
        }

        /// <summary>
        /// Read from the stream and deserialize into an object of type T (assuming Json content).
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <returns>An object of type T</returns>
        public static async Task<T> ReadAndDeserializeFromJsonAsync<T>(
            this Stream stream,
            Encoding encoding)
        {
            return await ReadAndDeserializeFromJsonAsync<T>(stream, encoding, true,
                Defaults.DefaultBufferSizeOnRead, false);
        }

        /// <summary>
        /// Read from the stream and deserialize into an object of type T (assuming Json content).
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="detectEncodingFromByteOrderMarks">True to detect encoding from byte order marks, false otherwise</param>
        /// <returns>An object of type T</returns>
        public static async Task<T> ReadAndDeserializeFromJsonAsync<T>(
            this Stream stream,
            bool detectEncodingFromByteOrderMarks)
        {
            return await ReadAndDeserializeFromJsonAsync<T>(stream, new UTF8Encoding(),
                detectEncodingFromByteOrderMarks, Defaults.DefaultBufferSizeOnRead, false);
        }

        /// <summary>
        /// Read from the stream and deserialize into an object of type T (assuming Json content).
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="detectEncodingFromByteOrderMarks">True to detect encoding from byte order marks, false otherwise</param>
        /// <param name="bufferSize">The size of the buffer</param>
        /// <returns>An object of type T</returns>
        public static async Task<T> ReadAndDeserializeFromJsonAsync<T>(
            this Stream stream,
            Encoding encoding,
            bool detectEncodingFromByteOrderMarks,
            int bufferSize)
        {
            return await ReadAndDeserializeFromJsonAsync<T>(stream, encoding,
                detectEncodingFromByteOrderMarks, bufferSize, false);
        }

        /// <summary>
        /// Read from the stream and deserialize into an object of type T (assuming Json content).
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="detectEncodingFromByteOrderMarks">True to detect encoding from byte order marks, false otherwise</param>
        /// <param name="bufferSize">The size of the buffer</param>
        /// <param name="leaveOpen">True to leave the stream open after the (internally used) StreamReader object is disposed</param>
        /// <returns>An object of type T</returns>
        public static async Task<T> ReadAndDeserializeFromJsonAsync<T>(
            this Stream stream,
            Encoding encoding,
            bool detectEncodingFromByteOrderMarks,
            int bufferSize,
            bool leaveOpen)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new NotSupportedException("Can't read from this stream.");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            using var streamReader = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
            using var jsonTextReader = new JsonTextReader(streamReader);
            var jToken = await JToken.LoadAsync(jsonTextReader);
            return jToken.ToObject<T>();
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <typeparam name="T">The type the object to serialize/write</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
        /// <param name="jsonSerializerSettings">The JsonSerialization setting beeing used for serialization. In default state it will use default settings from Newtonsoft.Json.JsonConvert.DefaultSettings</param>
        public static async Task SerializeToJsonAndWriteAsync<T>(
            this Stream stream,
            T objectToWrite,
            CancellationToken cancellationToken = default,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, false, cancellationToken, jsonSerializerSettings);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <typeparam name="T">The type the object to serialize/write</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
        /// <param name="jsonSerializerSettings">The JsonSerialization setting beeing used for serialization. In default state it will use default settings from Newtonsoft.Json.JsonConvert.DefaultSettings</param>
        public static async Task SerializeToJsonAndWriteAsync<T>(
            this Stream stream,
            T objectToWrite,
            Encoding encoding,
            CancellationToken cancellationToken = default,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, false, cancellationToken, jsonSerializerSettings);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <typeparam name="T">The type the object to serialize/write</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="bufferSize">The size of the buffer</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
        /// <param name="jsonSerializerSettings">The JsonSerialization setting beeing used for serialization. In default state it will use default settings from Newtonsoft.Json.JsonConvert.DefaultSettings</param>
        public static async Task SerializeToJsonAndWriteAsync<T>(
            this Stream stream,
            T objectToWrite,
            Encoding encoding,
            int bufferSize,
            CancellationToken cancellationToken = default,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, encoding, bufferSize, false, false, cancellationToken, jsonSerializerSettings);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <typeparam name="T">The type the object to serialize/write</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="bufferSize">The size of the buffer</param>
        /// <param name="leaveOpen">True to leave the stream open after the (internally used) StreamWriter object is disposed</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
        /// <param name="jsonSerializerSettings">The JsonSerialization setting beeing used for serialization. In default state it will use default settings from Newtonsoft.Json.JsonConvert.DefaultSettings</param>
        public static async Task SerializeToJsonAndWriteAsync<T>(
            this Stream stream,
            T objectToWrite,
            Encoding encoding,
            int bufferSize,
            bool leaveOpen,
            CancellationToken cancellationToken = default,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, encoding, bufferSize, leaveOpen, false, cancellationToken, jsonSerializerSettings);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <typeparam name="T">The type the object to serialize/write</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="resetStream">True to reset the stream to position 0 after writing, false otherwise</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
        /// <param name="jsonSerializerSettings">The JsonSerialization setting beeing used for serialization. In default state it will use default settings from Newtonsoft.Json.JsonConvert.DefaultSettings</param>
        public static async Task SerializeToJsonAndWriteAsync<T>(
            this Stream stream,
            T objectToWrite,
            bool resetStream,
            CancellationToken cancellationToken = default,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, resetStream, cancellationToken, jsonSerializerSettings);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <typeparam name="T">The type the object to serialize/write</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="resetStream">True to reset the stream to position 0 after writing, false otherwise</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
        /// <param name="jsonSerializerSettings">The JsonSerialization setting beeing used for serialization. In default state it will use default settings from Newtonsoft.Json.JsonConvert.DefaultSettings</param>
        public static async Task SerializeToJsonAndWriteAsync<T>(
            this Stream stream,
            T objectToWrite,
            Encoding encoding,
            bool resetStream,
            CancellationToken cancellationToken = default,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, resetStream, cancellationToken, jsonSerializerSettings);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <typeparam name="T">The type the object to serialize/write</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="bufferSize">The size of the buffer</param>
        /// <param name="leaveOpen">True to leave the stream open after the (internally used) StreamWriter object is disposed</param>
        /// <param name="resetStream">True to reset the stream to position 0 after writing, false otherwise</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
        /// <param name="jsonSerializerSettings">The JsonSerialization setting beeing used for serialization. In default state it will use default settings from Newtonsoft.Json.JsonConvert.DefaultSettings</param>
        public static async Task SerializeToJsonAndWriteAsync<T>(
            this Stream stream,
            T objectToWrite,
            Encoding encoding,
            int bufferSize,
            bool leaveOpen,
            bool resetStream,
            CancellationToken cancellationToken = default,
            JsonSerializerSettings jsonSerializerSettings = default)
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

            using var streamWriter = new StreamWriter(stream, encoding, bufferSize, leaveOpen);
            using var jsonTextWriter = new JsonTextWriter(streamWriter);
            var jsonSerializer = jsonSerializerSettings == default ? JsonSerializer.Create() : JsonSerializer.Create(jsonSerializerSettings);
            jsonSerializer.Serialize(jsonTextWriter, objectToWrite);
            await jsonTextWriter.FlushAsync(cancellationToken);


            // after writing, set the stream to position 0
            if (resetStream && stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
        /// <param name="jsonSerializerSettings">The JsonSerialization setting beeing used for serialization. In default state it will use default settings from Newtonsoft.Json.JsonConvert.DefaultSettings</param>
        public static async Task SerializeToJsonAndWriteAsync(
            this Stream stream,
            object objectToWrite,
            CancellationToken cancellationToken = default,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, false, cancellationToken, jsonSerializerSettings);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
        /// <param name="jsonSerializerSettings">The JsonSerialization setting beeing used for serialization. In default state it will use default settings from Newtonsoft.Json.JsonConvert.DefaultSettings</param>
        public static async Task SerializeToJsonAndWriteAsync(
            this Stream stream,
            object objectToWrite,
            Encoding encoding,
            CancellationToken cancellationToken = default,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, false, cancellationToken, jsonSerializerSettings);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="bufferSize">The size of the buffer</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
        /// <param name="jsonSerializerSettings">The JsonSerialization setting beeing used for serialization. In default state it will use default settings from Newtonsoft.Json.JsonConvert.DefaultSettings</param>
        public static async Task SerializeToJsonAndWriteAsync(
            this Stream stream,
            object objectToWrite,
            Encoding encoding,
            int bufferSize,
            CancellationToken cancellationToken = default,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, encoding, bufferSize, false, false, cancellationToken, jsonSerializerSettings);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="bufferSize">The size of the buffer</param>
        /// <param name="leaveOpen">True to leave the stream open after the (internally used) StreamWriter object is disposed</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
        /// <param name="jsonSerializerSettings">The JsonSerialization setting beeing used for serialization. In default state it will use default settings from Newtonsoft.Json.JsonConvert.DefaultSettings</param>
        public static async Task SerializeToJsonAndWriteAsync(
            this Stream stream,
            object objectToWrite,
            Encoding encoding,
            int bufferSize,
            bool leaveOpen,
            CancellationToken cancellationToken = default,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, encoding, bufferSize, leaveOpen, false, cancellationToken, jsonSerializerSettings);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="resetStream">True to reset the stream to position 0 after writing, false otherwise</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
        /// <param name="jsonSerializerSettings">The JsonSerialization setting beeing used for serialization. In default state it will use default settings from Newtonsoft.Json.JsonConvert.DefaultSettings</param>
        public static async Task SerializeToJsonAndWriteAsync(
            this Stream stream,
            object objectToWrite,
            bool resetStream,
            CancellationToken cancellationToken = default,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, resetStream, cancellationToken, jsonSerializerSettings);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="resetStream">True to reset the stream to position 0 after writing, false otherwise</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
        /// <param name="jsonSerializerSettings">The JsonSerialization setting beeing used for serialization. In default state it will use default settings from Newtonsoft.Json.JsonConvert.DefaultSettings</param>
        public static async Task SerializeToJsonAndWriteAsync(
            this Stream stream,
            object objectToWrite,
            Encoding encoding,
            bool resetStream,
            CancellationToken cancellationToken = default,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, resetStream, cancellationToken, jsonSerializerSettings);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="bufferSize">The size of the buffer</param>
        /// <param name="leaveOpen">True to leave the stream open after the (internally used) StreamWriter object is disposed</param>
        /// <param name="resetStream">True to reset the stream to position 0 after writing, false otherwise</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
        /// <param name="jsonSerializerSettings">The JsonSerialization setting beeing used for serialization. In default state it will use default settings from Newtonsoft.Json.JsonConvert.DefaultSettings</param>
        public static async Task SerializeToJsonAndWriteAsync(
            this Stream stream,
            object objectToWrite,
            Encoding encoding,
            int bufferSize,
            bool leaveOpen,
            bool resetStream,
            CancellationToken cancellationToken = default,
            JsonSerializerSettings jsonSerializerSettings = default)
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

            using var streamWriter = new StreamWriter(stream, encoding, bufferSize, leaveOpen);
            using var jsonTextWriter = new JsonTextWriter(streamWriter);
            var jsonSerializer = jsonSerializerSettings == default ? JsonSerializer.Create() : JsonSerializer.Create(jsonSerializerSettings);
            jsonSerializer.Serialize(jsonTextWriter, objectToWrite);
            await jsonTextWriter.FlushAsync(cancellationToken);

            // after writing, set the stream to position 0
            if (resetStream && stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }
        }
    }
}
