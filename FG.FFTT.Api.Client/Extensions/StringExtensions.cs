using System;
using System.Linq;
using System.Xml.Serialization;

namespace FG.FFTT.Api.Client.Extensions
{
    /// <summary>
    /// Extension methods for the String.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Transforms a key/value parameters string into a specific object.
        /// The key must correspond to a property of the object with the value of the XmlElement attribute.
        /// </summary>
        /// <typeparam name="T">Type of object for which an instance is created from the parameters</typeparam>
        /// <param name="parameters">Contains a list of parameters in the format "key1=value1&key2=value2"</param>
        /// <returns></returns>
        public static T ToObject<T>(this string parameters) where T: IXmlQueryParameters
        {
            T obj = (T)Activator.CreateInstance(typeof(T));

            var properties = typeof(T).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(XmlElementAttribute)));

            string[] keyvalues = parameters.Split("&");
            foreach (var parameter in keyvalues)
            {
                var keyvalue = parameter.Split("=");
                if (keyvalue.Length != 2) continue;

                var key = keyvalue[0];
                var value = keyvalue[1];

                var propInfo = properties.SingleOrDefault(prop => ((XmlElementAttribute)prop.GetCustomAttributes(typeof(XmlElementAttribute), true)?.First()).ElementName == key);
                if (propInfo == null) continue;

                propInfo.SetValue(obj, value);
            }

            return obj;
        }
    }
}