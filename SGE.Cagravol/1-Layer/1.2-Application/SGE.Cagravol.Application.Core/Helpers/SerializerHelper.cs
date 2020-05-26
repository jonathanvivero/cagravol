using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Core.Helpers
{
	public static class SerializerHelper
	{
		/// <summary>
		/// Serializes the specified email view model.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="objectToSerialize">The email view model.</param>
		/// <returns></returns>
		public static byte[] Serialize<T>(T objectToSerialize)
		{
			BinaryFormatter bf = new BinaryFormatter();
			byte[] result = null;
			using (MemoryStream ms = new MemoryStream())
			{
				using (GZipStream cs = new GZipStream(ms, CompressionMode.Compress))
				{
					bf.Serialize(ms, objectToSerialize);
				}
				result = ms.ToArray();
			}
			return result;
		}

		/// <summary>
		/// Deserializes the specified binary email.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="binaryToDeserialize">The binary email.</param>
		/// <returns></returns>
		public static T Deserialize<T>(byte[] binaryToDeserialize)
		{
			BinaryFormatter bf = new BinaryFormatter();
			T serializedObject;
			using (MemoryStream ms = new MemoryStream(binaryToDeserialize))
			{
				using (GZipStream cs = new GZipStream(ms, CompressionMode.Decompress))
				{
					using (MemoryStream outputStream = new MemoryStream())
					{
						ms.CopyTo(outputStream);
						outputStream.Seek(0, SeekOrigin.Begin);
						serializedObject = ((T)bf.Deserialize(outputStream));
					}
				}
			}
			return serializedObject;
		}
	}
}
