using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CodeGenerater.Translation
{
	public static class DataIntergration
	{
		#region Method
		public static void Save(ISerializable Instance)
		{
			MemoryStream Stream = null;
		
			try
			{
				Stream = new MemoryStream();
				(new BinaryFormatter()).Serialize(Stream, Instance);
				Properties.Settings.Default.IntergratedData = ToBase64(Stream.GetBuffer());
			}
			finally
			{
				Stream?.Close();
			}
		}

		public static ISerializable Load()
		{
			MemoryStream Stream = null;

			try
			{
				Stream = new MemoryStream(FromBase64(Properties.Settings.Default.IntergratedData));
				Stream.Position = 0;
				return (ISerializable)(new BinaryFormatter()).Deserialize(Stream);
			}
			finally
			{
				Stream?.Close();
			}
		}
		#endregion

		#region Helper
		static string ToBase64(byte[] Data)
		{
			return Convert.ToBase64String(Data);
		}

		static byte[] FromBase64(string Base64)
		{
			return Convert.FromBase64String(Base64);
		}
		#endregion
	}
}