using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Marvin.StreamExtensions
{
    /// <summary>
    /// Extensions for working with streams
    /// </summary>
    public static partial class StreamExtensions
    {
        public static T ReadAndDeserializeFromJson<T>(this Stream stream)
        {
            return ReadAndDeserializeFromJson<T>(stream, new UTF8Encoding(), true, 
                Defaults.DefaultBufferSizeOnRead, false);
        }

        public static T ReadAndDeserializeFromJson<T>(this Stream stream, Encoding encoding)
        {
            return ReadAndDeserializeFromJson<T>(stream, encoding, true,
                Defaults.DefaultBufferSizeOnRead, false);
        }

        public static T ReadAndDeserializeFromJson<T>(this Stream stream,
           bool detectEncodingFromByteOrderMarks)
        {
            return ReadAndDeserializeFromJson<T>(stream, new UTF8Encoding(),
                detectEncodingFromByteOrderMarks, Defaults.DefaultBufferSizeOnRead, false);
        }

        public static T ReadAndDeserializeFromJson<T>(this Stream stream,
            Encoding encoding,
            bool detectEncodingFromByteOrderMarks,
            int bufferSize)
        {
            return ReadAndDeserializeFromJson<T>(stream, encoding,
                detectEncodingFromByteOrderMarks, bufferSize, false);
        }

        public static T ReadAndDeserializeFromJson<T>(this Stream stream,
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

            using (var streamReader = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen))
            {
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    var jsonSerializer = new JsonSerializer();
                    return jsonSerializer.Deserialize<T>(jsonTextReader);
                }
            }
        }


        public static object ReadAndDeserializeFromJson(this Stream stream)
        {
            return ReadAndDeserializeFromJson(stream, new UTF8Encoding(), true,
                Defaults.DefaultBufferSizeOnRead, false);
        }

        public static object ReadAndDeserializeFromJson(this Stream stream, Encoding encoding)
        {
            return ReadAndDeserializeFromJson(stream, encoding, true,
                Defaults.DefaultBufferSizeOnRead, false);
        }

        public static object ReadAndDeserializeFromJson(this Stream stream,
           bool detectEncodingFromByteOrderMarks)
        {
            return ReadAndDeserializeFromJson(stream, new UTF8Encoding(),
                detectEncodingFromByteOrderMarks, Defaults.DefaultBufferSizeOnRead, false);
        }

        public static object ReadAndDeserializeFromJson(this Stream stream,
            Encoding encoding,
            bool detectEncodingFromByteOrderMarks,
            int bufferSize)
        {
            return ReadAndDeserializeFromJson(stream, encoding,
                detectEncodingFromByteOrderMarks, bufferSize, false);
        }

        public static object ReadAndDeserializeFromJson(this Stream stream,
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

            using (var streamReader = new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen))
            {
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    var jsonSerializer = new JsonSerializer();
                    return jsonSerializer.Deserialize(jsonTextReader);
                }
            }
        }


        public static void SerializeAndWriteToJson<T>(this Stream stream, T objectToWrite)
        {
            SerializeAndWriteToJson<T>(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, false);
        }

        public static void SerializeAndWriteToJson<T>(this Stream stream, T objectToWrite,
          Encoding encoding)
        {
            SerializeAndWriteToJson<T>(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, false);
        }

        public static void SerializeAndWriteToJson<T>(this Stream stream, T objectToWrite,
         Encoding encoding, int bufferSize)
        {
            SerializeAndWriteToJson<T>(stream, objectToWrite, encoding, bufferSize, false, false);
        }

        public static void SerializeAndWriteToJson<T>(this Stream stream, T objectToWrite,
        Encoding encoding, int bufferSize, bool leaveOpen)
        {
            SerializeAndWriteToJson<T>(stream, objectToWrite, encoding, bufferSize, leaveOpen, false);
        }

        public static void SerializeAndWriteToJson<T>(this Stream stream, T objectToWrite, bool resetStream)
        {
            SerializeAndWriteToJson<T>(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, resetStream);
        }

        public static void SerializeAndWriteToJson<T>(this Stream stream, T objectToWrite,
      Encoding encoding, bool resetStream)
        {
            SerializeAndWriteToJson<T>(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, resetStream);
        }


        public static void SerializeAndWriteToJson<T>(this Stream stream, T objectToWrite,
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
                    jsonTextWriter.Flush();
                }
            }

            // after writing, set the stream to position 0
            if (resetStream & stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }
        }

        public static void SerializeAndWriteToJson(this Stream stream, object objectToWrite)
        {
            SerializeAndWriteToJson(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, false);
        }

        public static void SerializeAndWriteToJson(this Stream stream, object objectToWrite,
          Encoding encoding)
        {
            SerializeAndWriteToJson(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, false);
        }

        public static void SerializeAndWriteToJson(this Stream stream, object objectToWrite,
         Encoding encoding, int bufferSize)
        {
            SerializeAndWriteToJson(stream, objectToWrite, encoding, bufferSize, false, false);
        }

        public static void SerializeAndWriteToJson(this Stream stream, object objectToWrite,
        Encoding encoding, int bufferSize, bool leaveOpen)
        {
            SerializeAndWriteToJson(stream, objectToWrite, encoding, bufferSize, leaveOpen, false);
        }

        public static void SerializeAndWriteToJson(this Stream stream, object objectToWrite, bool resetStream)
        {
            SerializeAndWriteToJson(stream, objectToWrite, new UTF8Encoding(), Defaults.DefaultBufferSizeOnWrite, false, resetStream);
        }

        public static void SerializeAndWriteToJson(this Stream stream, object objectToWrite,
        Encoding encoding, bool resetStream)
        {
            SerializeAndWriteToJson(stream, objectToWrite, encoding, Defaults.DefaultBufferSizeOnWrite, false, resetStream);
        }
        
        public static void SerializeAndWriteToJson(this Stream stream, object objectToWrite,
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
                    jsonTextWriter.Flush();
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
