using System.Xml.Serialization;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Returns the ranking of a team championship group.
    /// Object returned in response to request http://www.fftt.com/mobile/pxml/xml_result_equ.php.
    /// With the parameter "action" equal to "classement".
    /// </summary>
    [XmlRoot("liste")]
    public class GroupRankingResponse
    {
        [XmlElement("classement")]
        public GroupRanking[] GroupsRanking { get; set; }
    }

    /// <summary>
    /// Contains the information of the team in the group.
    /// </summary>
    public class GroupRanking
    {
        /// <summary>
        /// Group's identifier
        /// </summary>
        [XmlElement("poule")]
        public int GroupId { get; set; }

        /// <summary>
        /// Position of the team in the group
        /// </summary>
        [XmlElement("clt")]
        public int Position { get; set; }

        /// <summary>
        /// Team's name
        /// </summary>
        [XmlElement("equipe")]
        public string Team { get; set; }

        /// <summary>
        /// Number of games played
        /// </summary>
        [XmlElement("joue")]
        public int GamesNumber { get; set; }

        /// <summary>
        /// Number of points
        /// </summary>
        [XmlElement("pts")]
        public int Points { get; set; }

        /// <summary>
        /// Club's number
        /// </summary>
        [XmlElement("numero")]
        public string ClubNumber { get; set; }

        /// <summary>
        /// Total points of games won
        /// </summary>
        [XmlElement("totvic")]
        public int TotalWonPoints { get; set; }

        /// <summary>
        /// Total points of games lost
        /// </summary>
        [XmlElement("totdef")]
        public int TotalLostPoints { get; set; }

        /// <summary>
        /// Team's identifier
        /// </summary>
        [XmlElement("idequipe")]
        public int TeamId { get; set; }

        /// <summary>
        /// Club's identifier
        /// </summary>
        [XmlElement("idclub")]
        public int ClubId { get; set; }

        /// <summary>
        /// Number of wins
        /// </summary>
        [XmlElement("vic")]
        public int WinsNumber { get; set; }

        /// <summary>
        /// Number of defeats
        /// </summary>
        [XmlElement("def")]
        public int DefeatsNumber { get; set; }

        /// <summary>
        /// Number of ties
        /// </summary>
        [XmlElement("nul")]
        public int TiesNumber { get; set; }

        /// <summary>
        /// Number of penalties or forfeits
        /// </summary>
        [XmlElement("pf")]
        public int PenaltiesNumber { get; set; }

        /// <summary>
        /// Total games won
        /// </summary>
        [XmlElement("pg")]
        public int TotalWonGames { get; set; }

        /// <summary>
        /// Total games lost
        /// </summary>
        [XmlElement("pp")]
        public int TotalLostGames { get; set; }
    }
}