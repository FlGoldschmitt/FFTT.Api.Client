using System.Xml.Serialization;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Returns a list of players from the ranking database.
    /// Object returned in response to request http://www.fftt.com/mobile/pxml/xml_liste_joueur.php.
    /// </summary>
    [XmlRoot("liste")]
    public class PlayersRankingLightResponse
    {
        [XmlElement("joueur")]
        public PlayerRankingLight[] PlayersRanking { get; set; }
    }

    /// <summary>
    /// Contains the basic information of a player's ranking.
    /// </summary>
    public class PlayerRankingLight
    {
        /// <summary>
        /// Licence's number
        /// </summary>
        [XmlElement("licence")]
        public int Licence { get; set; }

        /// <summary>
        /// Player's last name
        /// </summary>
        [XmlElement("nom")]
        public string LastName { get; set; }

        /// <summary>
        /// Player's first name
        /// </summary>
        [XmlElement("prenom")]
        public string FirstName { get; set; }

        /// <summary>
        /// Club's name
        /// </summary>
        [XmlElement("club")]
        public string Club { get; set; }

        /// <summary>
        /// Club's number
        /// </summary>
        [XmlElement("nclub")]
        public string ClubNumber { get; set; }

        /// <summary>
        /// Official ranking
        /// </summary>
        [XmlElement("clast")]
        public string OfficialRanking { get; set; }
    }
}