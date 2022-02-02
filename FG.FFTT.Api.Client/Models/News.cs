using System.Xml.Serialization;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Returns the FFTT news feed.
    /// Object returned in response to request http://www.fftt.com/mobile/pxml/xml_new_actu.php.
    /// </summary>
    [XmlRoot("liste")]
    public class NewsResponse
    {
        [XmlElement("news")]
        public News[] News { get; set; }
    }

    /// <summary>
    /// Contains a news item from the FFTT.
    /// </summary>
    public class News
    {
        /// <summary>
        /// Date of publication of the news
        /// </summary>
        [XmlElement("date")]
        public string Date { get; set; }

        /// <summary>
        /// Title of the news
        /// </summary>
        [XmlElement("titre")]
        public string Title { get; set; }

        /// <summary>
        /// Description of the news
        /// </summary>
        [XmlElement("description")]
        public string Description { get; set; }

        /// <summary>
        /// Url of the news
        /// </summary>
        [XmlElement("url")]
        public string Url { get; set; }

        /// <summary>
        /// Picture of the news
        /// </summary>
        [XmlElement("photo")]
        public string Picture { get; set; }

        /// <summary>
        /// Category of the news
        /// </summary>
        [XmlElement("categorie")]
        public string Category { get; set; }
    }
}