using System.Xml.Serialization;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Contains the parameters of a game to be passed when calling the GetGameInformationsAsync method.
    /// </summary>
    public class GameParameters : XmlQueryParameters
    {
        /// <summary>
        /// Game's identifier
        /// </summary>
        [XmlElement("renc_id")]
        public string GameId { get; set; }

        /// <summary>
        /// Is the match a rematch?
        /// </summary>
        [XmlElement("is_retour")]
        public string IsReturn { get; set; }

        /// <summary>
        /// Game's phase
        /// </summary>
        [XmlElement("phase")]
        public string Phase { get; set; }

        /// <summary>
        /// Team's A result
        /// </summary>
        [XmlElement("res_1")]
        public string TeamAResult { get; set; }

        /// <summary>
        /// Team's B result
        /// </summary>
        [XmlElement("res_2")]
        public string TeamBResult { get; set; }

        /// <summary>
        /// Team's A name
        /// </summary>
        [XmlElement("equip_1")]
        public string TeamAName { get; set; }

        /// <summary>
        /// Team's B name
        /// </summary>
        [XmlElement("equip_2")]
        public string TeamBName { get; set; }

        /// <summary>
        /// Team's A identifier
        /// </summary>
        [XmlElement("equip_id1")]
        public string TeamAIdentifier { get; set; }

        /// <summary>
        /// Team's B identifier
        /// </summary>
        [XmlElement("equip_id2")]
        public string TeamBIdentifier { get; set; }
    }
}