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

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <typeparam name="T">The type the object to serialize/write</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        public static async Task SerializeToJsonAndWriteAsync<T>(
            this Stream stream, 
            T objectToWrite)
        {
            await SerializeToJsonAndWriteAsync<T>(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, false);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <typeparam name="T">The type the object to serialize/write</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        public static async Task SerializeToJsonAndWriteAsync<T>(
            this Stream stream, 
            T objectToWrite,
            Encoding encoding)
        {
            await SerializeToJsonAndWriteAsync<T>(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, false);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <typeparam name="T">The type the object to serialize/write</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="bufferSize">The size of the buffer</param>
        public static async Task SerializeToJsonAndWriteAsync<T>(
            this Stream stream, 
            T objectToWrite,
            Encoding encoding, 
            int bufferSize)
        {
            await SerializeToJsonAndWriteAsync<T>(stream, objectToWrite, encoding, bufferSize, false, false);
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
        public static async Task SerializeToJsonAndWriteAsync<T>(
            this Stream stream, 
            T objectToWrite,
            Encoding encoding, 
            int bufferSize, 
            bool leaveOpen)
        {
            await SerializeToJsonAndWriteAsync<T>(stream, objectToWrite, encoding, bufferSize, leaveOpen, false);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <typeparam name="T">The type the object to serialize/write</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="resetStream">True to reset the stream to position 0 after writing, false otherwise</param>
        public static async Task SerializeToJsonAndWriteAsync<T>(
            this Stream stream,
            T objectToWrite, 
            bool resetStream)
        {
            await SerializeToJsonAndWriteAsync<T>(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, resetStream);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <typeparam name="T">The type the object to serialize/write</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="resetStream">True to reset the stream to position 0 after writing, false otherwise</param>
        public static async Task SerializeToJsonAndWriteAsync<T>(
            this Stream stream, 
            T objectToWrite,
            Encoding encoding, 
            bool resetStream)
        {
            await SerializeToJsonAndWriteAsync<T>(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, resetStream);
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
        public static async Task SerializeToJsonAndWriteAsync<T>(
            this Stream stream, 
            T objectToWrite,
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

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        public static async Task SerializeToJsonAndWriteAsync(
            this Stream stream, 
            object objectToWrite)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, false);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        public static async Task SerializeToJsonAndWriteAsync(
            this Stream stream,
            object objectToWrite, 
            Encoding encoding)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, false);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <typeparam name="T">The type the object to serialize/write</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="bufferSize">The size of the buffer</param>
        public static async Task SerializeToJsonAndWriteAsync(
            this Stream stream, 
            object objectToWrite,
            Encoding encoding, 
            int bufferSize)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, encoding, bufferSize, false, false);
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
        public static async Task SerializeToJsonAndWriteAsync(
            this Stream stream, 
            object objectToWrite,
            Encoding encoding, 
            int bufferSize,
            bool leaveOpen)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, encoding, bufferSize, leaveOpen, false);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="resetStream">True to reset the stream to position 0 after writing, false otherwise</param>
        public static async Task SerializeToJsonAndWriteAsync(
            this Stream stream, 
            object objectToWrite,
            bool resetStream)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, resetStream);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="resetStream">True to reset the stream to position 0 after writing, false otherwise</param>
        public static async Task SerializeToJsonAndWriteAsync(
            this Stream stream, 
            object objectToWrite,
            Encoding encoding,
            bool resetStream)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, resetStream);
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
        public static async Task SerializeToJsonAndWriteAsync(
            this Stream stream, 
            object objectToWrite,
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
