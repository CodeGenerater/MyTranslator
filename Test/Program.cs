using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Test
{
	class Program
	{
		static void Main(string[] args)
		{
			Dictionary<string, string> Dictionary = new XmlSerializableDictionary<string, string>();

			Dictionary.Add("Hello", "World");

			MemoryStream Stream = new MemoryStream();
			XmlSerializer Serializer = new XmlSerializer(typeof(XmlSerializableDictionary<string, string>));

			Serializer.Serialize(Stream, Dictionary);

			Stream.Position = 0;
			Dictionary = (XmlSerializableDictionary<string, string>)Serializer.Deserialize(Stream);

			Console.WriteLine(Dictionary["Hello"]);
			Stream.Dispose();
		}
	}
}