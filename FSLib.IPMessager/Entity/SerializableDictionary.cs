using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace FSLib.IPMessager.Entity
{
	/// <summary>
	/// 支持XML序列化的泛型Dictionary类
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	[XmlRoot("GenericDictionary")]
	public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, System.Xml.Serialization.IXmlSerializable
	{
		#region IXmlSerializable Members

		public System.Xml.Schema.XmlSchema GetSchema()
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// 从对象的XML表示形式生成该对象
		/// </summary>
		/// <param name="reader"></param>
		public void ReadXml(XmlReader reader)
		{
			XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
			XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
			bool wasEmpty = reader.IsEmptyElement;
			reader.Read();

			if (wasEmpty)
				return;
			while (reader.NodeType != XmlNodeType.EndElement)
			{
				reader.ReadStartElement("Key");
				TKey key = (TKey)keySerializer.Deserialize(reader);
				reader.ReadEndElement();

				reader.ReadStartElement("Value");
				TValue value = (TValue)valueSerializer.Deserialize(reader);
				reader.ReadEndElement();
				this.Add(key, value);
				reader.MoveToContent();
			}
			reader.ReadEndElement();
		}


		/// <summary>
		/// 将对象转换为其XML表示形式
		/// </summary>
		/// <param name="writer"></param>
		public void WriteXml(XmlWriter writer)
		{
			XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
			XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
			foreach (TKey key in this.Keys)
			{
				writer.WriteStartElement("Key");
				keySerializer.Serialize(writer, key);
				writer.WriteEndElement();

				writer.WriteStartElement("Value");
				TValue value = this[key];
				valueSerializer.Serialize(writer, value);
				writer.WriteEndElement();
			}
		}

		#endregion
	}
}
