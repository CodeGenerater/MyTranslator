using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeGenerater.Translation;

namespace UnitTest
{
	[TestClass]
	[Serializable]
	public class SerializationHelperTest : ISerializable
	{
		#region Serializaion
		public void GetObjectData(SerializationInfo Info, StreamingContext Context)
		{
			SerializationHelper.Serialize(this, Info);
		}

		SerializationHelperTest(SerializationInfo Info, StreamingContext Conmtext)
		{
			SerializationHelper.Deserialize(this, Info);
		}
		#endregion

		#region Constructor
		public SerializationHelperTest()
		{

		}
		#endregion

		#region Property
		[SerializationTarget]
		public int PublicProperty
		{
			set;
			get;
		}

		[SerializationTarget]
		public int HalfPublicProperty
		{
			private set;
			get;
		}

		[SerializationTarget]
		int PrivateProperty
		{
			set;
			get;
		}
		#endregion

		#region Method
		[TestMethod()]
		public void Test_SerializaionHelper()
		{
			var T = new SerializationHelperTest()
			{
				PublicProperty = 10,
				HalfPublicProperty = 20,
				PrivateProperty = 30,
			};

			using (MemoryStream Stream = new MemoryStream())
			{
				BinaryFormatter F = new BinaryFormatter();

				F.Serialize(Stream, T); T = null;

				Stream.Position = 0;

				T = (SerializationHelperTest)F.Deserialize(Stream);

				Assert.IsTrue(T.PublicProperty == 10 && T.HalfPublicProperty == 20 && T.PrivateProperty == 30);
			}
		}
		#endregion
	}
}