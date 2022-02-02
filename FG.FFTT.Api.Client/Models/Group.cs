using System.Xml.Serialization;

using FG.FFTT.Api.Client.Extensions;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Returns the list of groups in the division.
    /// Object returned in response to request http://www.fftt.com/mobile/pxml/xml_result_equ.php.
    /// With the parameter "action" equal to "poule".
    /// </summary>
    [XmlRoot("liste")]
    public class GroupsResponse
    {
        [XmlElement("poule")]
        public Group[] Groups { get; set; }
    }

    /// <summary>
    /// Contains the basic information of a group.
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Group's label
        /// </summary>
        [XmlElement("libelle")]
        public string Label { get; set; }

        /// <summary>
        /// Link containing the group id and division id
        /// </summary>
        [XmlElement("lien")]
        public string Link { get; set; }

        /// <summary>
        /// Transforms the parameters string of the Link property into an object.
        /// </summary>
        [XmlIgnore]
        public GroupParameters Parameters => Link.ToObject<GroupParameters>();
    }
}