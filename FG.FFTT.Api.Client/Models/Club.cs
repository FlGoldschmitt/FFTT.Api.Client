using System.Xml.Serialization;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Returns the list of clubs.
    /// Object returned in response to request http://www.fftt.com/mobile/pxml/xml_club_dep2.php (only with departement parameter)
    /// Or object returned in response to request http://www.fftt.com/mobile/pxml/xml_club_b.php
    /// </summary>
    [XmlRoot("liste")]
    public class ClubsResponse
    {
        [XmlElement("club")]
        public Club[] Clubs { get; set; }
    }

    /// <summary>
    /// Represents the basic information of a club.
    /// </summary>
    public class Club
    {
        /// <summary>
        /// Club's unique identifier
        /// </summary>
        [XmlElement("idclub")]
        public int Id { get; set; }

        /// <summary>
        /// Club's number
        /// </summary>
        [XmlElement("numero")]
        public string Number { get; set; }

        /// <summary>
        /// Club's name
        /// </summary>
        [XmlElement("nom")]
        public string Name { get; set; }

        /// <summary>
        /// Date of validation of the club
        /// </summary>
        [XmlElement("validation")]
        public string ValidationDate { get; set; }

        /// <summary>
        /// Club's type (L: Ligue ?)
        /// </summary>
        [XmlElement("typeclub")]
        public string Type { get; set; }
    }
}