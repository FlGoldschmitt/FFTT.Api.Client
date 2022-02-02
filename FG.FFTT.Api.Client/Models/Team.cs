using System.Xml.Serialization;

using FG.FFTT.Api.Client.Extensions;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Returns a list of a club's teams.
    /// Object returned in response to request http://www.fftt.com/mobile/pxml/xml_equipe.php.
    /// </summary>
    [XmlRoot("liste")]
    public class TeamsResponse
    {
        [XmlElement("equipe")]
        public Team[] Teams { get; set; }
    }

    /// <summary>
    /// Contains the information of an team.
    /// </summary>
    public class Team
    {
        /// <summary>
        /// Event's identifier
        /// </summary>
        [XmlElement("idepr")]
        public int EventId { get; set; }

        /// <summary>
        /// Event's name
        /// </summary>
        [XmlElement("libepr")]
        public string EventLabel { get; set; }

        /// <summary>
        /// Team's identifier
        /// </summary>
        [XmlElement("idequipe")]
        public int TeamId { get; set; }

        /// <summary>
        /// Team's name
        /// </summary>
        [XmlElement("libequipe")]
        public string TeamLabel { get; set; }

        /// <summary>
        /// Division's name
        /// </summary>
        [XmlElement("libdivision")]
        public string DivisionLabel { get; set; }

        /// <summary>
        /// Link containing the group id (cx_poule) and the id (D1) of the division.
        /// </summary>
        [XmlElement("liendivision")]
        public string DivisionLink { get; set; }

        /// <summary>
        /// Transforms the parameters string of the Link property into an object.
        /// </summary>
        [XmlIgnore]
        public TeamParameters Parameters => DivisionLink.ToObject<TeamParameters>();
    }
}