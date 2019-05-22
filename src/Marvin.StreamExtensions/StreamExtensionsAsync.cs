using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
		public static async Task SerializeToJsonAndWriteAsync<T>(
            this Stream stream, 
            T objectToWrite,
			CancellationToken cancellationToken = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, false, cancellationToken);
        }

		/// <summary>
		/// Serialize (to Json) and write to the stream
		/// </summary>
		/// <typeparam name="T">The type the object to serialize/write</typeparam>
		/// <param name="stream">The stream</param>
		/// <param name="objectToWrite">The object to write to the stream</param>
		/// <param name="encoding">The encoding to use</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
		public static async Task SerializeToJsonAndWriteAsync<T>(
            this Stream stream, 
            T objectToWrite,
            Encoding encoding,
			CancellationToken cancellationToken = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, false, cancellationToken);
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
        public static async Task SerializeToJsonAndWriteAsync<T>(
            this Stream stream, 
            T objectToWrite,
            Encoding encoding, 
            int bufferSize,
			CancellationToken cancellationToken = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, encoding, bufferSize, false, false, cancellationToken);
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
        public static async Task SerializeToJsonAndWriteAsync<T>(
            this Stream stream, 
            T objectToWrite,
            Encoding encoding, 
            int bufferSize, 
            bool leaveOpen,
			CancellationToken cancellationToken = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, encoding, bufferSize, leaveOpen, false, cancellationToken);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <typeparam name="T">The type the object to serialize/write</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="resetStream">True to reset the stream to position 0 after writing, false otherwise</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
        public static async Task SerializeToJsonAndWriteAsync<T>(
            this Stream stream,
            T objectToWrite, 
            bool resetStream,
			CancellationToken cancellationToken = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, resetStream, cancellationToken);
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
        public static async Task SerializeToJsonAndWriteAsync<T>(
            this Stream stream, 
            T objectToWrite,
            Encoding encoding, 
            bool resetStream,
			CancellationToken cancellationToken = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, resetStream, cancellationToken);
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
        public static async Task SerializeToJsonAndWriteAsync<T>(
            this Stream stream, 
            T objectToWrite,
            Encoding encoding,
            int bufferSize,
            bool leaveOpen,
            bool resetStream,
			CancellationToken cancellationToken = default)
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
                    await jsonTextWriter.FlushAsync(cancellationToken);
                }
            }

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
        public static async Task SerializeToJsonAndWriteAsync(
            this Stream stream, 
            object objectToWrite,
			CancellationToken cancellationToken = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, false, cancellationToken);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
        public static async Task SerializeToJsonAndWriteAsync(
            this Stream stream,
            object objectToWrite, 
            Encoding encoding,
			CancellationToken cancellationToken = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, false, cancellationToken);
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
        public static async Task SerializeToJsonAndWriteAsync(
            this Stream stream, 
            object objectToWrite,
            Encoding encoding, 
            int bufferSize,
			CancellationToken cancellationToken = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, encoding, bufferSize, false, false, cancellationToken);
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
        public static async Task SerializeToJsonAndWriteAsync(
            this Stream stream, 
            object objectToWrite,
            Encoding encoding, 
            int bufferSize,
            bool leaveOpen,
			CancellationToken cancellationToken = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, encoding, bufferSize, leaveOpen, false, cancellationToken);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="resetStream">True to reset the stream to position 0 after writing, false otherwise</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
        public static async Task SerializeToJsonAndWriteAsync(
            this Stream stream, 
            object objectToWrite,
            bool resetStream,
			CancellationToken cancellationToken = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, resetStream, cancellationToken);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="resetStream">True to reset the stream to position 0 after writing, false otherwise</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
        public static async Task SerializeToJsonAndWriteAsync(
            this Stream stream, 
            object objectToWrite,
            Encoding encoding,
            bool resetStream,
			CancellationToken cancellationToken = default)
        {
            await SerializeToJsonAndWriteAsync(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, resetStream, cancellationToken);
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
        public static async Task SerializeToJsonAndWriteAsync(
            this Stream stream, 
            object objectToWrite,
            Encoding encoding,
            int bufferSize,
            bool leaveOpen,
            bool resetStream,
			CancellationToken cancellationToken = default)
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
                    await jsonTextWriter.FlushAsync(cancellationToken);
                }
            }

            // after writing, set the stream to position 0
            if (resetStream && stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }
        }
    }
}
