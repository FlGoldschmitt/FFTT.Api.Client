using System.Xml.Serialization;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Returns the player's ranking history.
    /// Object returned in response to request http://www.fftt.com/mobile/pxml/xml_histo_classement.php.
    /// </summary>
    [XmlRoot("liste")]
    public class PlayerHistoryResponse
    {
        [XmlElement("histo")]
        public PlayerHistory[] PlayerHistory { get; set; }
    }

    /// <summary>
    /// Returns the player's ranking history for a period (season + phase 1 or 2).
    /// </summary>
    public class PlayerHistory
    {
        /// <summary>
        /// "N" if classified national or nothing
        /// </summary>
        [XmlElement("echelon")]
        public string Level { get; set; }

        /// <summary>
        /// Player's number if nationally ranked
        /// </summary>
        [XmlElement("place")]
        public string Place { get; set; }

        /// <summary>
        /// Number of player's points
        /// </summary>
        [XmlElement("point")]
        public int Points { get; set; }

        /// <summary>
        /// Season's label
        /// </summary>
        [XmlElement("saison")]
        public string Season { get; set; }

        /// <summary>
        /// Phase indicator (1 or 2)
        /// </summary>
        [XmlElement("phase")]
        public int Phase { get; set; }
    }
}