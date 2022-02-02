using System.Xml.Serialization;

using FG.FFTT.Api.Client.Extensions;

namespace FG.FFTT.Api.Client.Models
{
    /// <summary>
    /// Returns the results of one or more team championship groups.
    /// Object returned in response to request http://www.fftt.com/mobile/pxml/xml_result_equ.php 
    /// Or object returned in response to request http://www.fftt.com/mobile/pxml/xml_rencontre_equ.php
    /// With the parameter "action" equal to "" (empty).
    /// </summary>
    [XmlRoot("liste")]
    public class GamesResultResponse
    {
        [XmlElement("tour")]
        public GameResult[] GamesResult { get; set; }
    }

    /// <summary>
    /// Contains the information about a game between 2 teams.
    /// </summary>
    public class GameResult
    {
        /// <summary>
        /// Name of the group, round and date
        /// </summary>
        [XmlElement("libelle")]
        public string Label { get; set; }

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
        /// Team A score
        /// </summary>
        [XmlElement("scorea")]
        public string ScoreA { get; set; }

        /// <summary>
        /// Team B score
        /// </summary>
        [XmlElement("scoreb")]
        public string ScoreB { get; set; }

        /// <summary>
        /// String of parameters to be passed to access the details of the game
        /// </summary>
        [XmlElement("lien")]
        public string Link { get; set; }

        /// <summary>
        /// Transforms the parameters string of the Link property into an object.
        /// </summary>
        [XmlIgnore]
        public GameParameters Parameters => Link.ToObject<GameParameters>();

        /// <summary>
        /// Planned date of the game
        /// </summary>
        [XmlElement("dateprevue")]
        public string PlannedDate { get; set; }

        /// <summary>
        /// Actual date of the game
        /// </summary>
        [XmlElement("datereelle")]
        public string ActualDate { get; set; }

        #region only for request "xml_rencontre_equ.php"

        /// <summary>
        /// Number of the club A
        /// </summary>
        [XmlElement("ncluba")]
        public string ClubANumber { get; set; }

        /// <summary>
        /// Number of the club B
        /// </summary>
        [XmlElement("nclubb")]
        public string ClubBNumber { get; set; }

        /// <summary>
        /// Group's identifier
        /// </summary>
        [XmlElement("poule")]
        public int GroupId { get; set; }

        /// <summary>
        /// 0 or 1 if live existing
        /// </summary>
        [XmlElement("live")]
        public bool isLive { get; set; }

        #endregion
    }
}