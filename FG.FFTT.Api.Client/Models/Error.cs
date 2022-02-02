using System;
using System.Xml.Serialization;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Type of error returned when an error is raised by the API.
    /// For example when a required parameter is empty or incorrect.
    /// </summary>
    [Serializable]
    [XmlRoot("erreurs")]
    public class Error
    {
        /// <summary>
        /// Error code
        /// </summary>
        [XmlElement("code")]
        public string Code { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        [XmlElement("erreur")]
        public string Message { get; set; }
    }

    /// <summary>
    /// Type of error returned when the identification parameters (serie, tm, tmc and id) are missing.
    /// </summary>
    [Serializable]
    [XmlRoot("user")]
    public class UnauthorizedAccess
    {
        /// <summary>
        /// Determines if access is allowed or not
        /// </summary>
        [XmlElement("verification")]
        public bool IsAuthorized { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        [XmlElement("erreur")]
        public string Message { get; set; }
    }
}