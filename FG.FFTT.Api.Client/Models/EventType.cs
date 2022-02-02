using System.Xml.Serialization;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Returns a list of events for an organization.
    /// Object returned in response to request http://www.fftt.com/mobile/pxml/xml_epreuve.php.
    /// </summary>
    [XmlRoot("liste")]
    public class EventsTypeResponse
    {
        [XmlElement("epreuve")]
        public EventType[] EventsType { get; set; }
    }

    /// <summary>
    /// Contains the information of an event.
    /// </summary>
    public class EventType
    {
        /// <summary>
        /// Event's identifier
        /// </summary>
        [XmlElement("idepreuve")]
        public int EventId { get; set; }

        /// <summary>
        /// Organisation's identifier
        /// </summary>
        [XmlElement("idorga")]
        public int OrganizationId { get; set; }

        /// <summary>
        /// Event's name
        /// </summary>
        [XmlElement("libelle")]
        public string Label { get; set; }

        /// <summary>
        /// Event's type (C=Critérium, I=Individual event, E=French Team Championship, H=Others by teams)
        /// </summary>
        [XmlElement("typepreuve")]
        public string Type { get; set; }
    }
}