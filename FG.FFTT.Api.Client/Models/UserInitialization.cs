using System;
using System.Xml.Serialization;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Verifies and initializes a new API user.
    /// Object returned in response to request http://www.fftt.com/mobile/pxml/xml_initialisation.php.
    /// </summary>
    [Serializable]
    [XmlRoot("initialisation")]
    public class UserInitialization
    {
        /// <summary>
        /// Determines if access is allowed or not
        /// </summary>
        [XmlElement("appli")]
        public bool IsAuthorized { get; set; }
    }
}