using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Marvin.StreamExtensions
{
	public static class ResponseMessageExtensionsAsync
	{
		/// <summary>
		/// Reads content of HttpResponseMessage as a stream and deserializes into an object of type T (assuming Json content).
		/// </summary>
		/// <typeparam name="T">The object type</typeparam>
		/// <param name="message">The HTTP response message</param>
		/// <returns>An object of type T</returns>
		public static async Task<T> DeserializeAsStreamAsync<T>(
			this HttpResponseMessage message)
		{
			return await DeserializeAsStreamAsync<T>(
					message,
					new UTF8Encoding(),
					true,
					Defaults.DefaultBufferSizeOnRead, 
					false);
		}

		/// <summary>
		/// Reads content of HttpResponseMessage as a stream and deserializes into an object of type T (assuming Json content).
		/// </summary>
		/// <typeparam name="T">The object type</typeparam>
		/// <param name="message">The HTTP response message</param>
		/// <param name="encoding">The encoding to use</param>
		/// <returns>An object of type T</returns>
		public static async Task<T> DeserializeAsStreamAsync<T>(
			this HttpResponseMessage message,
			Encoding encoding)
		{
			return await DeserializeAsStreamAsync<T>(
					message,
					encoding,
					true,
					Defaults.DefaultBufferSizeOnRead, 
					false);
		}

		/// <summary>
		/// Reads content of HttpResponseMessage as a stream and deserializes into an object of type T (assuming Json content).
		/// </summary>
		/// <typeparam name="T">The object type</typeparam>
		/// <param name="message">The HTTP response message</param>
		/// <param name="detectEncodingFromByteOrderMarks">True to detect encoding from byte order marks, false otherwise</param>
		/// <returns>An object of type T</returns>
		public static async Task<T> DeserializeAsStreamAsync<T>(
			this HttpResponseMessage message,
			bool detectEncodingFromByteOrderMarks)
		{
			return await DeserializeAsStreamAsync<T>(
					message,
					new UTF8Encoding(),
					detectEncodingFromByteOrderMarks,
					Defaults.DefaultBufferSizeOnRead,
					false);
		}

		/// <summary>
		/// Reads content of HttpResponseMessage as a stream and deserializes into an object of type T (assuming Json content).
		/// </summary>
		/// <typeparam name="T">The object type</typeparam>
		/// <param name="message">The HTTP response message</param>
		/// <param name="encoding">The encoding to use</param>
		/// <param name="detectEncodingFromByteOrderMarks">True to detect encoding from byte order marks, false otherwise</param>
		/// <param name="bufferSize">The size of the buffer</param>
		/// <returns>An object of type T</returns>
		public static async Task<T> DeserializeAsStreamAsync<T>(
			this HttpResponseMessage message,
			Encoding encoding,
			bool detectEncodingFromByteOrderMarks,
			int bufferSize)
		{
			return await DeserializeAsStreamAsync<T>(
					message,
					encoding,
					detectEncodingFromByteOrderMarks,
					bufferSize, 
					false);
		}

		/// <summary>
		/// Reads content of HttpResponseMessage as a stream and deserializes into an object of type T (assuming Json content).
		/// </summary>
		/// <typeparam name="T">The object type</typeparam>
		/// <param name="message">The HTTP response message</param>
		/// <param name="encoding">The encoding to use</param>
		/// <param name="detectEncodingFromByteOrderMarks">True to detect encoding from byte order marks, false otherwise</param>
		/// <param name="bufferSize">The size of the buffer</param>
		/// <param name="leaveOpen">True to leave the stream open after the (internally used) StreamReader object is disposed</param>
		/// <returns>An object of type T</returns>
		public static async Task<T> DeserializeAsStreamAsync<T>(
			this HttpResponseMessage message,
			Encoding encoding,
			bool detectEncodingFromByteOrderMarks,
			int bufferSize,
			bool leaveOpen)
		{
			var stream = await message.Content.ReadAsStreamAsync();
			return await stream.ReadAndDeserializeFromJsonAsync<T>(
					encoding,
					detectEncodingFromByteOrderMarks,
					bufferSize,
					leaveOpen);
		}
	}
}