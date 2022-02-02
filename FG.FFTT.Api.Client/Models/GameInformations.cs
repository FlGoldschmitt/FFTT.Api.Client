using System.Xml.Serialization;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Returns the detailed information of a game.
    /// Object returned in response to request http://www.fftt.com/mobile/pxml/xml_chp_renc.php 
    /// </summary>
    [XmlRoot("liste")]
    public class GameInformations
    {
        [XmlElement("resultat")]
        public GameResult Result { get; set; }

        [XmlElement("joueur")]
        public Player[] Players { get; set; }

        [XmlElement("partie")]
        public Match[] Matches { get; set; }

        /// <summary>
        /// Result of the game.
        /// </summary>
        public class GameResult
        {
            /// <summary>
            /// Name of the team A
            /// </summary>
            [XmlElement("equa")]
            public string TeamA { get; set; }

            /// <summary>
            /// Name of the team B
            /// </summary>
            [XmlElement("equb")]
            public string TeamB { get; set; }

            /// <summary>
            /// Result of the team A
            /// </summary>
            [XmlElement("resa")]
            public int TeamAResult { get; set; }

            /// <summary>
            /// Result of the team B
            /// </summary>
            [XmlElement("resb")]
            public int TeamBResult { get; set; }
        }

        /// <summary>
        /// Informations about the player in the game.
        /// </summary>
        public class Player
        {
            /// <summary>
            /// Player's name A
            /// </summary>
            [XmlElement("xja")]
            public string PlayerA { get; set; }

            /// <summary>
            /// Player's ranking A
            /// </summary>
            [XmlElement("xca")]
            public string RankingA { get; set; }

            /// <summary>
            /// Player's name B
            /// </summary>
            [XmlElement("xjb")]
            public string PlayerB { get; set; }

            /// <summary>
            /// Player's ranking B
            /// </summary>
            [XmlElement("xcb")]
            public string RankingB { get; set; }
        }

        /// <summary>
        /// Represents a match between a player from Team A and Team B.
        /// </summary>
        public class Match
        {
            /// <summary>
            /// Player's name A
            /// </summary>
            [XmlElement("ja")]
            public string PlayerA { get; set; }

            /// <summary>
            /// Player's score A
            /// </summary>
            [XmlElement("scorea")]
            public int ScoreA { get; set; }

            /// <summary>
            /// Player's name B
            /// </summary>
            [XmlElement("jb")]
            public string PlayerB { get; set; }

            /// <summary>
            /// Player's score B
            /// </summary>
            [XmlElement("scoreb")]
            public int ScoreB { get; set; }

            /// <summary>
            /// Details of the sets of the match
            /// </summary>
            [XmlElement("detail")]
            public string Sets { get; set; }
        }
    }
}