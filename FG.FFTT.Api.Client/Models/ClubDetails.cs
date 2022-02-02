using System.Xml.Serialization;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Returns the details for a club.
    /// Object returned in response to request http://www.fftt.com/mobile/pxml/xml_club_detail.php.
    /// </summary>
    [XmlRoot("liste")]
    public class ClubDetailsResponse
    {
        [XmlElement("club")]
        public ClubDetails ClubDetails { get; set; }
    }

    /// <summary>
    /// Contains additional information about the club, such as the gym address and contact details.
    /// </summary>
    public class ClubDetails
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
        /// Gym's name
        /// </summary>
        [XmlElement("nomsalle")]
        public string GymName { get; set; }

        /// <summary>
        /// Gym's address 1 
        /// </summary>
        [XmlElement("adressesalle1")]
        public string GymAddress1 { get; set; }

        /// <summary>
        /// Gym's address 2
        /// </summary>
        [XmlElement("adressesalle2")]
        public string GymAddress2 { get; set; }

        /// <summary>
        /// Gym's address 3
        /// </summary>
        [XmlElement("adressesalle3")]
        public string GymAddress3 { get; set; }

        /// <summary>
        /// Gym's postcode
        /// </summary>
        [XmlElement("codepsalle")]
        public string GymPostcode { get; set; }

        /// <summary>
        /// Gym's city
        /// </summary>
        [XmlElement("villesalle")]
        public string GymCity { get; set; }

        /// <summary>
        /// Club's website
        /// </summary>
        [XmlElement("web")]
        public string Website { get; set; }

        /// <summary>
        /// Name of club contact
        /// </summary>
        [XmlElement("nomcor")]
        public string ContactName { get; set; }

        /// <summary>
        /// Firstname of club contact
        /// </summary>
        [XmlElement("prenomcor")]
        public string ContactFistname { get; set; }

        /// <summary>
        /// Mail of club contact
        /// </summary>
        [XmlElement("mailcor")]
        public string ContactMail { get; set; }

        /// <summary>
        /// Phone number of club contact
        /// </summary>
        [XmlElement("telcor")]
        public string ContactPhone { get; set; }

        /// <summary>
        /// Club's latitude
        /// </summary>
        [XmlElement("latitude")]
        public string Latitude { get; set; }

        /// <summary>
        /// Club's longitude
        /// </summary>
        [XmlElement("longitude")]
        public string Longitude { get; set; }

        /// <summary>
        /// Date of validation of the club
        /// </summary>
        [XmlElement("validation")]
        public string ValidationDate { get; set; }
    }
}