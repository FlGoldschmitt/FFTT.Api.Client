using System.Xml.Serialization;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Contains the parameters of a group.
    /// </summary>
    public class GroupParameters : XmlQueryParameters
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
    }
}