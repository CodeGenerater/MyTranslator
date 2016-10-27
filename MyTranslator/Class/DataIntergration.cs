using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CodeGenerater.Translation
{
	public class DataIntergration
	{
		#region Field
		 XElement _Xml;
		#endregion

		#region Property
		 XElement Xml
		{
			get
			{
				if (_Xml == null)
				{
					if (string.IsNullOrEmpty(Properties.Settings.Default.__Xml__))
						_Xml = new XElement("Instances");
					else
						_Xml = XElement.Parse(Properties.Settings.Default.__Xml__);

					_Xml.Changed += XmlChanged;
				}
				
				return _Xml;
			}
		}
		#endregion

		#region Method
		public  T GetInstance<T>(string Key) where T : ISerializable
		{
			var Data = (from e in Xml.Descendants()
						where e.Attribute("Key").Value == Key
						select e.Element("Data").Value).FirstOrDefault();
			
			if (Data == null)
				throw new NullReferenceException();

			return (T)SerializationHelper.Deserialize(FromBase64(Data));
		}

		public  void AddInstance<T>(string Key, T Instance) where T : ISerializable
		{
			if (ContainsKey(Key))
				throw new ArgumentException("Key");

			Xml.Add(new XElement("Instance", new XAttribute("Key", Key), new XElement("Data", ToBase64(SerializationHelper.Serialize(Instance)))));
		}

		public  bool ContainsKey(string Key)
		{
			return (from e in Xml.Descendants()
					from a in e.Attributes()
					where a.Name == "Key" && a.Value == Key
					select e).Count() > 0;
		}

		public  void RemoveInstance(string Key)
		{

		}
		#endregion

		#region Helper
		 string ToBase64(byte[] Data)
		{
			return Convert.ToBase64String(Data);
		}

		 byte[] FromBase64(string Base64)
		{
			return Convert.FromBase64String(Base64);
		}
		#endregion

		#region Event Handler
		private  void XmlChanged(object sender, XObjectChangeEventArgs e)
		{
			MemoryStream Stream = new MemoryStream();
			Xml.Save(Stream);
			byte[] Buffer = Stream.GetBuffer();
			Properties.Settings.Default.__Xml__ = Encoding.Unicode.GetString(Buffer);
			Stream.Close();
		}
		#endregion
	}
}