using System;
using System.Xml.Serialization;

using FG.FFTT.Api.Client.Extensions;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Contains the identification parameters that must be passed in each request.
    /// </summary>
    public class Token : XmlQueryParameters
    {
        #region properties

        /// <summary>
        /// Parameter "id" in the query
        /// </summary>
        [XmlElement("id")]
        public string Id { get; private set; }

        /// <summary>
        /// Parameter "serie" in the query
        /// </summary>
        [XmlElement("serie")]
        public string SerialNumber { get; private set; }

        /// <summary>
        /// Parameter "tm" in the query
        /// </summary>
        [XmlElement("tm")]
        public string Timestamp { get; private set; }

        /// <summary>
        /// Parameter "tmc" in the query
        /// </summary>
        [XmlElement("tmc")]
        public string TimestampEncoded { get; private set; }

        #endregion properties

        #region public methods

        /// <summary>
        /// Generates a virtual "Token" which contains the 4 parameters to be passed in all requests (id, serial, tm, tmc)
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public static Token Create(FfttCredentials credentials)
        {
            var timestamp = DateTime.Now.GetTimestamp();

            return new Token()
            {
                Id = credentials.Id,
                SerialNumber = credentials.SerialNumber,
                Timestamp = timestamp,
                TimestampEncoded = credentials.EncodeTimestampToSha1(timestamp)
            };
        }

        #endregion public methods
    }
}