using System.Xml.Serialization;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Returns a player's licence from the SPID database (and information on its monthly progress).
    /// Object returned in response to request http://www.fftt.com/mobile/pxml/xml_licence.php.
    /// Or object returned in response to request http://www.fftt.com/mobile/pxml/xml_licence_b.php.
    /// </summary>
    [XmlRoot("liste")]
    public class PlayerLicenceResponse
    {
        [XmlElement("licence")]
        public PlayerLicence PlayerLicence { get; set; }
    }

    /// <summary>
    /// Contains the basic information of a player's licence.
    /// </summary>
    public class PlayerLicence
    {
        /// <summary>
        /// Licence's identifier
        /// </summary>
        [XmlElement("idlicence")]
        public int Id { get; set; }

        /// <summary>
        /// Licence's number
        /// </summary>
        [XmlElement("licence")]
        public int Number { get; set; }

        /// <summary>
        /// Player's lastname
        /// </summary>
        [XmlElement("nom")]
        public string Lastname { get; set; }

        /// <summary>
        /// Player's firstname
        /// </summary>
        [XmlElement("prenom")]
        public string Firstname { get; set; }

        /// <summary>
        /// Club's number
        /// </summary>
        [XmlElement("numclub")]
        public string ClubNumber { get; set; }

        /// <summary>
        /// Club's name
        /// </summary>
        [XmlElement("nomclub")]
        public string ClubName { get; set; }

        /// <summary>
        /// Player's gender (M / F)
        /// </summary>
        [XmlElement("sexe")]
        public string Gender { get; set; }

        /// <summary>
        /// Licence's type (T / P)
        /// </summary>
        [XmlElement("type")]
        public string Type { get; set; }

        /// <summary>
        /// Medical certificate (Certificate, Neither training nor competition, Quadruple)
        /// </summary>
        [XmlElement("certif")]
        public string Certificate { get; set; }

        /// <summary>
        /// Validation date
        /// </summary>
        [XmlElement("validation")]
        public string ValidationDate { get; set; }

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
        [XmlElement("point")]
        public double Points { get; set; }

        /// <summary>
        /// Player's age category
        /// </summary>
        [XmlElement("cat")]
        public string Category { get; set; }

        #region only for request "xml_licence_b.php"

        /// <summary>
        /// Player's monthly points
        /// </summary>
        [XmlElement("pointm")]
        public double MonthlyPoints { get; set; }

        /// <summary>
        /// Player's old monthly points
        /// </summary>
        [XmlElement("apointm")]
        public double OldMonthlyPoints { get; set; }

        /// <summary>
        /// Player's initial points
        /// </summary>
        [XmlElement("initm")]
        public double InitialPoints { get; set; }

        /// <summary>
        /// Transfer date
        /// </summary>
        [XmlElement("mutation")]
        public string TransferDate { get; set; }

        /// <summary>
        /// Player's nationality
        /// </summary>
        [XmlElement("natio")]
        public string Nationality { get; set; }

        /// <summary>
        /// Last referee grade
        /// </summary>
        [XmlElement("arb")]
        public string LastRefereeGrade { get; set; }

        /// <summary>
        /// Last umpire grade
        /// </summary>
        [XmlElement("ja")]
        public string LastUmpireGrade { get; set; }

        /// <summary>
        /// Last technician grade
        /// </summary>
        [XmlElement("tech")]
        public string LastTechnicianGrade { get; set; }

        #endregion only for request "xml_licence_b.php"
    }
}