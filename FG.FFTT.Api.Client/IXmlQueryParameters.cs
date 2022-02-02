using System;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace FG.FFTT.Api.Client
{
    /// <summary>
    /// Represents an object defined by properties with XmlElement attributes.
    /// This object is used to pass its values in the query parameters.
    /// </summary>
    public interface IXmlQueryParameters
    {
        Dictionary<string, object> GetQueryParameters();
    }

    public abstract class XmlQueryParameters : IXmlQueryParameters
    {
        /// <summary>
        /// Retrieves all properties with an XmlElement attribute and creates a key/value dictionary to prepare the query parameters.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetQueryParameters()
        {
            var dictionary = new Dictionary<string, object>();

            var properties = GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(XmlElementAttribute)));
            foreach (var property in properties)
            {
                string attributName = ((XmlElementAttribute)property.GetCustomAttributes(typeof(XmlElementAttribute), true)?.First()).ElementName;
                object value = property.GetValue(this);

                dictionary.Add(attributName, value);
            }

            return dictionary;
        }
    }
}