using System.IO;
using System.Xml.Serialization;

namespace FG.FFTT.Api.Client.Extensions
{
    /// <summary>
    /// XML utility class for string to object conversion and vice versa.
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        /// Deserializes the xml content to string format as an object.
        /// </summary>
        /// <typeparam name="T">Type of the object to be deserialized</typeparam>
        /// <param name="value">String format representing an XML content</param>
        /// <returns>The object to be deserialised</returns>
        public static T FromXml<T>(this string value)
        {
            using (var reader = new StringReader(value))
                return (T)new XmlSerializer(typeof(T)).Deserialize(reader);
        }

        /// <summary>
        /// Serialise the object as xml content in string format.
        /// </summary>
        /// <typeparam name="T">Type of the object to be serialised</typeparam>
        /// <param name="obj">The object to be serialised</param>
        /// <returns>String format representing an XML content</returns>
        public static string ToXml<T>(T obj)
        {
            using (var writer = new StringWriter())
            {
                new XmlSerializer(typeof(T)).Serialize(writer, obj);
                return writer.ToString();
            }
        }
    }
}