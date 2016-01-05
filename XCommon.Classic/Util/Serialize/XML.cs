using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace XCommon.Util.Serialize
{
    public static class XML
    {
        public static string Serialize<TEntity>(TEntity objeto)
        {
            XmlSerializer serialize = new XmlSerializer(typeof(TEntity));
            MemoryStream mStream = new MemoryStream();

            XmlTextWriter xmlWriter = new XmlTextWriter(mStream, new UTF8Encoding(false));

            serialize.Serialize(xmlWriter, objeto);
            mStream = (MemoryStream)xmlWriter.BaseStream;

            return UTF8ByteArrayToString(mStream.ToArray());
        }

        public static TEntity DeSerialize<TEntity>(string texto)
        {
            XmlSerializer serialize = new XmlSerializer(typeof(TEntity));
            MemoryStream mStream = new MemoryStream(StringToUTF8ByteArray(texto));
            XmlTextWriter xmlWriter = new XmlTextWriter(mStream, Encoding.UTF8);

            var o = (TEntity)serialize.Deserialize(mStream);
            return o;

        }

        #region Auxilizar
        private static string UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        private static byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }
        #endregion
    }
}
