using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Collections.Generic
{
	public class XmlSerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
	{
		#region Field
		static Dictionary<Type, XmlSerializer> SerializerDict = new Dictionary<Type, XmlSerializer>();
		#endregion

		#region Helper
		static XmlSerializer GetSerializer(Type T)
		{
			if (!SerializerDict.ContainsKey(T))
				SerializerDict.Add(T, new XmlSerializer(T));

			return SerializerDict[T];
		}
		#endregion

		#region IXmlSerializable
		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader Reader)
		{
			XmlSerializer KeySerializer = GetSerializer(typeof(TKey));
			XmlSerializer ValueSerializer = GetSerializer(typeof(TValue));

			if (Reader.IsEmptyElement)
				return;

			Reader.Read();

			while (Reader.NodeType != XmlNodeType.EndElement)
			{
				Reader.ReadStartElement("Pair");

				#region Read Key
				Reader.ReadStartElement("Key");
				TKey Key = (TKey)KeySerializer.Deserialize(Reader);
				Reader.ReadEndElement();
				#endregion

				#region Read Value
				Reader.ReadStartElement("Value");
				TValue Value = (TValue)ValueSerializer.Deserialize(Reader);
				Reader.ReadEndElement();
				#endregion

				Add(Key, Value);

				Reader.ReadEndElement();
			}

			Reader.ReadEndElement();
		}

		public void WriteXml(XmlWriter Writer)
		{
			XmlSerializer KeySerializer = GetSerializer(typeof(TKey));
			XmlSerializer ValueSerializer = GetSerializer(typeof(TValue));

			foreach (var each in this)
			{
				Writer.WriteStartElement("Pair");

				#region Write Key
				Writer.WriteStartElement("Key");
				KeySerializer.Serialize(Writer, each.Key);
				Writer.WriteEndElement();
				#endregion

				#region Write Value
				Writer.WriteStartElement("Value");
				ValueSerializer.Serialize(Writer, each.Value);
				Writer.WriteEndElement();
				#endregion

				Writer.WriteEndElement();
			}
		}
		#endregion
	}
}