using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Marvin.StreamExtensions
{
    /// <summary>
    /// Extension methods for working with streams
    /// </summary>
    public static partial class StreamExtensions
    {
        /// <summary>
        /// Read from the stream and deserialize into an object of type T (assuming Json content).
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="stream">The stream</param>
        /// <returns>An object of type T</returns>
        public static T ReadAndDeserializeFromJson<T>(
            this Stream stream, JsonSerializerSettings jsonSerializerSettings = default)
        {
            return ReadAndDeserializeFromJson<T>(stream, new UTF8Encoding(), true,
                Defaults.DefaultBufferSizeOnRead, false, jsonSerializerSettings);
        }

        /// <summary>
        /// Read from the stream and deserialize into an object of type T (assuming Json content).
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <returns>An object of type T</returns>
        public static T ReadAndDeserializeFromJson<T>(
            this Stream stream,
            Encoding encoding, JsonSerializerSettings jsonSerializerSettings = default)
        {
            return ReadAndDeserializeFromJson<T>(stream, encoding, true,
                Defaults.DefaultBufferSizeOnRead, false, jsonSerializerSettings);
        }

        /// <summary>
        /// Read from the stream and deserialize into an object of type T (assuming Json content).
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="detectEncodingFromByteOrderMarks">True to detect encoding from byte order marks, false otherwise</param>
        /// <returns>An object of type T</returns>
        public static T ReadAndDeserializeFromJson<T>(
            this Stream stream,
            bool detectEncodingFromByteOrderMarks,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            return ReadAndDeserializeFromJson<T>(stream, new UTF8Encoding(),
                detectEncodingFromByteOrderMarks, Defaults.DefaultBufferSizeOnRead, false, jsonSerializerSettings);
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
        public static T ReadAndDeserializeFromJson<T>(
            this Stream stream,
            Encoding encoding,
            bool detectEncodingFromByteOrderMarks,
            int bufferSize,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            return ReadAndDeserializeFromJson<T>(stream, encoding,
                detectEncodingFromByteOrderMarks, bufferSize, false, jsonSerializerSettings);
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
        public static T ReadAndDeserializeFromJson<T>(
            this Stream stream,
            Encoding encoding,
            bool detectEncodingFromByteOrderMarks,
            int bufferSize,
            bool leaveOpen,
            JsonSerializerSettings jsonSerializerSettings = default
            )
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (!stream.CanRead)
                throw new NotSupportedException("Can't read from this stream.");

            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));

            using var streamReader = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
            using var jsonTextReader = new JsonTextReader(streamReader);

            var jsonSerializer = jsonSerializerSettings == default ? JsonSerializer.Create() : JsonSerializer.Create(jsonSerializerSettings);
            return jsonSerializer.Deserialize<T>(jsonTextReader);
        }

        /// <summary>
        /// Read from the stream and deserialize (assuming Json content).
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <returns>The deserialized object</returns>
        public static object ReadAndDeserializeFromJson(
            this Stream stream,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            return ReadAndDeserializeFromJson(stream, new UTF8Encoding(), true,
                Defaults.DefaultBufferSizeOnRead, jsonSerializerSettings);
        }

        /// <summary>
        /// Read from the stream and deserialize (assuming Json content).
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <returns>The deserialized object</returns>
        public static object ReadAndDeserializeFromJson(
            this Stream stream,
            Encoding encoding,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            return ReadAndDeserializeFromJson(stream, encoding, true,
                Defaults.DefaultBufferSizeOnRead, false, jsonSerializerSettings);
        }

        /// <summary>
        /// Read from the stream and deserialize (assuming Json content).
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="detectEncodingFromByteOrderMarks">True to detect encoding from byte order marks, false otherwise</param>
        /// <returns>The deserialized object</returns>
        public static object ReadAndDeserializeFromJson(
            this Stream stream,
            bool detectEncodingFromByteOrderMarks,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            return ReadAndDeserializeFromJson(stream, new UTF8Encoding(),
                detectEncodingFromByteOrderMarks, Defaults.DefaultBufferSizeOnRead, false, jsonSerializerSettings);
        }


        /// <summary>
        /// Read from the stream and deserialize (assuming Json content).
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="detectEncodingFromByteOrderMarks">True to detect encoding from byte order marks, false otherwise</param>
        /// <param name="bufferSize">The size of the buffer</param>
        /// <returns>The deserialized object</returns>
        public static object ReadAndDeserializeFromJson(
            this Stream stream,
            Encoding encoding,
            bool detectEncodingFromByteOrderMarks,
            int bufferSize,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            return ReadAndDeserializeFromJson(stream, encoding,
                detectEncodingFromByteOrderMarks, bufferSize, false, jsonSerializerSettings);
        }

        /// <summary>
        /// Read from the stream and deserialize (assuming Json content).
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="detectEncodingFromByteOrderMarks">True to detect encoding from byte order marks, false otherwise</param>
        /// <param name="bufferSize">The size of the buffer</param>
        /// <param name="leaveOpen">True to leave the stream open after the (internally used) StreamReader object is disposed</param>
        /// <returns>The deserialized object</returns>
        public static object ReadAndDeserializeFromJson(
            this Stream stream,
            Encoding encoding,
            bool detectEncodingFromByteOrderMarks,
            int bufferSize,
            bool leaveOpen,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (!stream.CanRead)
                throw new NotSupportedException("Can't read from this stream.");

            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));

            using var streamReader = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
            using var jsonTextReader = new JsonTextReader(streamReader);

            var jsonSerializer = jsonSerializerSettings == default ? JsonSerializer.Create() : JsonSerializer.Create(jsonSerializerSettings);
            return jsonSerializer.Deserialize(jsonTextReader);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <typeparam name="T">The type the object to serialize/write</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        public static void SerializeToJsonAndWrite<T>(
            this Stream stream,
            T objectToWrite,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            SerializeToJsonAndWrite(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, false);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <typeparam name="T">The type the object to serialize/write</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        public static void SerializeToJsonAndWrite<T>(
            this Stream stream,
            T objectToWrite,
            Encoding encoding, JsonSerializerSettings jsonSerializerSettings = default)
        {
            SerializeToJsonAndWrite(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, false, jsonSerializerSettings);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <typeparam name="T">The type the object to serialize/write</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="bufferSize">The size of the buffer</param>
        public static void SerializeToJsonAndWrite<T>(
            this Stream stream,
            T objectToWrite,
            Encoding encoding,
            int bufferSize,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            SerializeToJsonAndWrite(stream, objectToWrite, encoding, bufferSize, false, false, jsonSerializerSettings);
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
        public static void SerializeToJsonAndWrite<T>(
            this Stream stream,
            T objectToWrite,
            Encoding encoding,
            int bufferSize,
            bool leaveOpen, JsonSerializerSettings jsonSerializerSettings = default)
        {
            SerializeToJsonAndWrite(stream, objectToWrite, encoding, bufferSize, leaveOpen, false, jsonSerializerSettings);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <typeparam name="T">The type the object to serialize/write</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="resetStream">True to reset the stream to position 0 after writing, false otherwise</param>
        public static void SerializeToJsonAndWrite<T>(
            this Stream stream,
            T objectToWrite,
            bool resetStream, JsonSerializerSettings jsonSerializerSettings = default)
        {
            SerializeToJsonAndWrite(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, resetStream, jsonSerializerSettings);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <typeparam name="T">The type the object to serialize/write</typeparam>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="resetStream">True to reset the stream to position 0 after writing, false otherwise</param>
        public static void SerializeToJsonAndWrite<T>(
            this Stream stream,
            T objectToWrite,
            Encoding encoding,
            bool resetStream, JsonSerializerSettings jsonSerializerSettings = default)
        {
            SerializeToJsonAndWrite(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, resetStream, jsonSerializerSettings);
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
        public static void SerializeToJsonAndWrite<T>(
            this Stream stream,
            T objectToWrite,
            Encoding encoding,
            int bufferSize,
            bool leaveOpen,
            bool resetStream,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (!stream.CanWrite)
                throw new NotSupportedException("Can't write to this stream.");

            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));

            using var streamWriter = new StreamWriter(stream, encoding, bufferSize, leaveOpen);
            using var jsonTextWriter = new JsonTextWriter(streamWriter);

            var jsonSerializer = jsonSerializerSettings == default ? JsonSerializer.Create() : JsonSerializer.Create(jsonSerializerSettings);
            jsonSerializer.Serialize(jsonTextWriter, objectToWrite);
            jsonTextWriter.Flush();

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
        public static void SerializeToJsonAndWrite(
            this Stream stream,
            object objectToWrite, JsonSerializerSettings jsonSerializerSettings = default)
        {
            SerializeToJsonAndWrite(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, false, jsonSerializerSettings);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        public static void SerializeToJsonAndWrite(
            this Stream stream,
            object objectToWrite,
            Encoding encoding, JsonSerializerSettings jsonSerializerSettings = default)
        {
            SerializeToJsonAndWrite(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, false, jsonSerializerSettings);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="bufferSize">The size of the buffer</param>
        public static void SerializeToJsonAndWrite(
            this Stream stream,
            object objectToWrite,
            Encoding encoding,
            int bufferSize,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            SerializeToJsonAndWrite(stream, objectToWrite, encoding, bufferSize, false, false, jsonSerializerSettings);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="bufferSize">The size of the buffer</param>
        /// <param name="leaveOpen">True to leave the stream open after the (internally used) StreamWriter object is disposed</param>
        public static void SerializeToJsonAndWrite(
            this Stream stream,
            object objectToWrite,
            Encoding encoding,
            int bufferSize,
            bool leaveOpen,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            SerializeToJsonAndWrite(stream, objectToWrite, encoding, bufferSize, leaveOpen, false, jsonSerializerSettings);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="resetStream">True to reset the stream to position 0 after writing, false otherwise</param>
        public static void SerializeToJsonAndWrite(
            this Stream stream,
            object objectToWrite,
            bool resetStream,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            SerializeToJsonAndWrite(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, resetStream, jsonSerializerSettings);
        }

        /// <summary>
        /// Serialize (to Json) and write to the stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="objectToWrite">The object to write to the stream</param>
        /// <param name="encoding">The encoding to use</param>
        /// <param name="resetStream">True to reset the stream to position 0 after writing, false otherwise</param>
        public static void SerializeToJsonAndWrite(
            this Stream stream,
            object objectToWrite,
            Encoding encoding,
            bool resetStream,
            JsonSerializerSettings jsonSerializerSettings = default)
        {
            SerializeToJsonAndWrite(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, resetStream, jsonSerializerSettings);
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
        public static void SerializeToJsonAndWrite(
            this Stream stream,
            object objectToWrite,
            Encoding encoding,
            int bufferSize,
            bool leaveOpen,
            bool resetStream,
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

            var jsonSerializer = jsonSerializerSettings == null ? JsonSerializer.Create() : JsonSerializer.Create(jsonSerializerSettings);
            jsonSerializer.Serialize(jsonTextWriter, objectToWrite);
            jsonTextWriter.Flush();

            // after writing, set the stream to position 0
            if (resetStream && stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }
        }
    }
}
