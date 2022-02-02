using System.Xml.Serialization;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Returns a list of a player's games from the SPID database.
    /// Object returned in response to request http://www.fftt.com/mobile/pxml/xml_partie.php.
    /// </summary>
    [XmlRoot("liste")]
    public class PlayerGamesSpidResponse
    {
        [XmlElement("partie")]
        public PlayerGameSpid[] PlayerGamesSpid { get; set; }
    }

    /// <summary>
    /// Contains a player's match information from the SPID database.
    /// </summary>
    public class PlayerGameSpid
    {
        /// <summary>
        /// Game's date
        /// </summary>
        [XmlElement("date")]
        public string Date { get; set; }

        /// <summary>
        /// Opponent's name
        /// </summary>
        [XmlElement("nom")]
        public string Name { get; set; }

        /// <summary>
        /// Opponent's ranking
        /// </summary>
        [XmlElement("classement")]
        public int Ranking { get; set; }

        /// <summary>
        /// Event's name
        /// </summary>
        [XmlElement("epreuve")]
        public string EventType { get; set; }

        /// <summary>
        /// Result: V for victory and D for defeat
        /// </summary>
        [XmlElement("victoire")]
        public string Result { get; set; }

        /// <summary>
        /// Forfeit indicator: 0 for no forfeit / 1 for forfeit
        /// </summary>
        [XmlElement("forfait")]
        public bool IsForfeit { get; set; }

        /// <summary>
        /// Game's identifier
        /// </summary>
        [XmlElement("idpartie")]
        public int GameId { get; set; }

        /// <summary>
        /// Event's coefficient
        /// </summary>
        [XmlElement("coefchamp")]
        public double Ratio { get; set; }
    }
}