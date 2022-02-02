using System.Xml.Serialization;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Returns a list of players from the spid database.
    /// Object returned in response to request http://www.fftt.com/mobile/pxml/xml_liste_joueur_o.php.
    /// </summary>
    [XmlRoot("liste")]
    public class PlayersResponse
    {
        [XmlElement("joueur")]
        public Player[] Players { get; set; }
    }

    /// <summary>
    /// Contain a player's basic information.
    /// </summary>
    public class Player
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
        /// Player's gender (M / F)
        /// </summary>
        [XmlElement("sexe")]
        public string Gender { get; set; }

        /// <summary>
        /// Player's level :"N" if classified national or nothing
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
        [XmlElement("points")]
        public string Points { get; set; }
    }
}