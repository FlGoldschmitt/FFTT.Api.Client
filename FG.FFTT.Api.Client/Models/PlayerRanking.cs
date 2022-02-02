using System.Xml.Serialization;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Represents a player's ranking from the ranking database.
    /// Object returned in response to request http://www.fftt.com/mobile/pxml/xml_joueur.php.
    /// </summary>
    [XmlRoot("liste")]
    public class PlayerRankingResponse
    {
        [XmlElement("joueur")]
        public PlayerRanking PlayerRanking { get; set; }
    }

    /// <summary>
    /// Contains full information on a player's ranking.
    /// </summary>
    public class PlayerRanking
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
        /// Nationality of the player (E = Foreign)
        /// </summary>
        [XmlElement("natio")]
        public string Nationality { get; set; }

        /// <summary>
        /// Global ranking
        /// </summary>
        [XmlElement("clglob")]
        public int Ranking { get; set; }

        /// <summary>
        /// Monthly points
        /// </summary>
        [XmlElement("point")]
        public double Points { get; set; }

        /// <summary>
        /// Old overall ranking
        /// </summary>
        [XmlElement("aclglob")]
        public int OldRanking { get; set; }

        /// <summary>
        /// Old points
        /// </summary>
        [XmlElement("apoint")]
        public double OldPoints { get; set; }

        /// <summary>
        /// Official ranking
        /// </summary>
        [XmlElement("clast")]
        public int OfficialRanking { get; set; }

        /// <summary>
        /// National ranking
        /// </summary>
        [XmlElement("clnat")]
        public int NationalRanking { get; set; }

        /// <summary>
        /// Player's age category
        /// </summary>
        [XmlElement("categ")]
        public string Category { get; set; }

        /// <summary>
        /// Regional ranking
        /// </summary>
        [XmlElement("rangreg")]
        public int RegionalRanking { get; set; }

        /// <summary>
        /// Departmental ranking
        /// </summary>
        [XmlElement("rangdep")]
        public int DepartmentalRanking { get; set; }

        /// <summary>
        /// Official points
        /// </summary>
        [XmlElement("valcla")]
        public double OfficialPoints { get; set; }

        /// <summary>
        /// Ranking proposal
        /// </summary>
        [XmlElement("clpro")]
        public int RankingProposal { get; set; }

        /// <summary>
        /// Points at the beginning of the season
        /// </summary>
        [XmlElement("valinit")]
        public double StartingPoint { get; set; }
    }
}