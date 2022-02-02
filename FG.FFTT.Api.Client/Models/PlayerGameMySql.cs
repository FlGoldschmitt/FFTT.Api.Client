using System.Xml.Serialization;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Returns a list of a player's games from the MySql database.
    /// Object returned in response to request http://www.fftt.com/mobile/pxml/xml_partie_mysql.php.
    /// </summary>
    [XmlRoot("liste")]
    public class PlayerGamesMySqlResponse
    {
        [XmlElement("partie")]
        public PlayerGameMySql[] PlayerGamesMySql { get; set; }
    }

    /// <summary>
    /// Contains a player's match information from the MySql database.
    /// </summary>
    public class PlayerGameMySql
    {
        /// <summary>
        /// Player's licence number
        /// </summary>
        [XmlElement("licence")]
        public int PlayerLicence { get; set; }

        /// <summary>
        /// Opponent's licence number
        /// </summary>
        [XmlElement("advlic")]
        public int OpponentLicence { get; set; }

        /// <summary>
        /// Result: V for victory and D for defeat
        /// </summary>
        [XmlElement("vd")]
        public string Result { get; set; }

        /// <summary>
        /// Day number of the game
        /// </summary>
        [XmlElement("numjourn")]
        public string DayNumber { get; set; }

        /// <summary>
        /// Championship code
        /// </summary>
        [XmlElement("codechamp")]
        public string ChampionshipCode { get; set; }

        /// <summary>
        /// Game's date
        /// </summary>
        [XmlElement("date")]
        public string Date { get; set; }

        /// <summary>
        /// Opponent's gender
        /// </summary>
        [XmlElement("advsexe")]
        public string OpponentGender { get; set; }

        /// <summary>
        /// Opponent's first name and last name
        /// </summary>
        [XmlElement("advnompre")]
        public string OpponentName { get; set; }

        /// <summary>
        /// Result points obtained for this game
        /// </summary>
        [XmlElement("pointres")]
        public double ResultPoints { get; set; }

        /// <summary>
        /// Event's coefficient
        /// </summary>
        [XmlElement("coefchamp")]
        public double Ratio { get; set; }

        /// <summary>
        /// Official ranking of the opponent
        /// </summary>
        [XmlElement("advclaof")]
        public int OpponentRanking { get; set; }

        /// <summary>
        /// Game's identifier
        /// </summary>
        [XmlElement("idpartie")]
        public int GameId { get; set; }
    }
}