using System.Xml.Serialization;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Returns a list of organizations.
    /// Object returned in response to request http://www.fftt.com/mobile/pxml/xml_organisme.php.
    /// </summary>
    [XmlRoot("liste")]
    public class OrganizationsResponse
    {
        [XmlElement("organisme")]
        public Organization[] Organizations { get; set; }
    }

    /// <summary>
    /// Contains the information of an organisation.
    /// </summary>
    public class Organization
    {
        /// <summary>
        /// Organisation's name
        /// </summary>
        [XmlElement("libelle")]
        public string Label { get; set; }

        /// <summary>
        /// Organisation's identifier
        /// </summary>
        [XmlElement("id")]
        public int Id { get; set; }

        /// <summary>
        /// Organisation's code
        /// </summary>
        [XmlElement("code")]
        public string Code { get; set; }

        /// <summary>
        /// Organisation's parent identifier
        /// </summary>
        [XmlElement("idPere")]
        public int ParentId { get; set; }
    }
}