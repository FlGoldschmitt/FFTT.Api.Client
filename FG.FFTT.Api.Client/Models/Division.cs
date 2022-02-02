using System.Xml.Serialization;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Returns a list of division for a given event.
    /// Object returned in response to request http://www.fftt.com/mobile/pxml/xml_division.php.
    /// </summary>
    [XmlRoot("liste")]
    public class DivisionsResponse
    {
        [XmlElement("division")]
        public Division[] Divisions { get; set; }
    }

    /// <summary>
    /// Contains the information of an division.
    /// </summary>
    public class Division
    {
        /// <summary>
        /// Division's identifier
        /// </summary>
        [XmlElement("iddivision")]
        public int Id { get; set; }

        /// <summary>
        /// Division's name
        /// </summary>
        [XmlElement("libelle")]
        public string Label { get; set; }
    }
}