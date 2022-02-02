using System.Xml.Serialization;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Contains the parameters of a team.
    /// </summary>
    public class TeamParameters : XmlQueryParameters
    {
        /// <summary>
        /// Group's identifier
        /// </summary>
        [XmlElement("cx_poule")]
        public string GroupId { get; set; }

        /// <summary>
        /// Division's identifier
        /// </summary>
        [XmlElement("D1")]
        public string DivisionId { get; set; }

        /// <summary>
        /// Organisation's parent identifier
        /// </summary>
        [XmlElement("organisme_pere")]
        public string OrganizationParentId { get; set; }
    }
}