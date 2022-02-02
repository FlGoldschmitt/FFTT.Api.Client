using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using FG.FFTT.Api.Client.Models;
using FG.FFTT.Api.Client.Exceptions;

namespace FG.FFTT.Api.Client
{
    /// <summary>
    /// Basic client to query the FFTT API.
    /// </summary>
    public class FfttClient : IDisposable
    {
        #region constants

        const string HOSTNAME = "https://www.fftt.com/mobile/pxml";

        #endregion constants

        #region private fields

        private Token _token;

        #endregion private fields

        #region constructor

        public FfttClient(FfttCredentials ffttCredentials)
        {
            // Generates a virtual "Token" which contains the 4 parameters to be passed in all requests (id, serial, tm, tmc)
            _token = Token.Create(ffttCredentials);
        }

        #endregion constructor

        #region public methods

        public async Task<T> ApiGetAsync<T>(string route, Dictionary<string, object> queryParameters = null)
        {
            // The properties of the virtual token are retrieved and added to the query parameters
            var tokenParameters = _token.GetQueryParameters();

            // The case where the key exists in both dictionaries is not handled
            queryParameters = queryParameters?.Union(tokenParameters).ToDictionary(t => t.Key, t => t.Value) ?? tokenParameters;

            using (var request = new FfttRequest(HOSTNAME))
                return await request
                    .SetVerb(HttpVerbs.Get)
                    .SetRoute(route)
                    .SetQueryParameters(queryParameters)
                    .ExecuteAsync<T>();
        }

        #region user initialisation

        /// <summary>
        /// Verifies and initializes a new API user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<UserInitialization> UserInitializationAsync()
        {
            return await ApiGetAsync<UserInitialization>("xml_initialisation.php");
        }

        #endregion user initialisation

        #region clubs

        /// <summary>
        /// Returns a club by its number.
        /// </summary>
        /// <param name="clubNumber">Club's number</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<Club> GetClubByNumberAsync(string clubNumber)
        {
            return
                (await ApiGetAsync<ClubsResponse>(
                    "xml_club_b.php",
                    new Dictionary<string, object>() { { "numero", clubNumber } }
                ))
                .Clubs?.First() ?? throw ApiException.NoResult();
        }

        /// <summary>
        /// Returns a club by its postal code.
        /// </summary>
        /// <param name="postalCode">Club's postal code</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<Club> GetClubByPostalCodeAsync(int postalCode)
        {
            return
                (await ApiGetAsync<ClubsResponse>(
                    "xml_club_b.php",
                    new Dictionary<string, object>() { { "code", postalCode } }
                ))
                .Clubs?.First() ?? throw ApiException.NoResult();
        }

        /// <summary>
        /// Returns the list of clubs for a department.
        /// </summary>
        /// <param name="departmentNumber">Number of the department searched according to the codification of the organization table</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<Club>> GetClubsByDepartmentAsync(int departmentNumber)
        {
            return
                (await ApiGetAsync<ClubsResponse>(
                    "xml_club_dep2.php",
                    new Dictionary<string, object>() { { "dep", departmentNumber } }
                ))
                .Clubs ?? throw ApiException.NoResult();
        }

        /// <summary>
        /// Returns the list of clubs for a city or club's name.
        /// </summary>
        /// <param name="city">Search city or club's name (search type "contains)</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<Club>> GetClubsByCityAsync(string city)
        {
            return
                (await ApiGetAsync<ClubsResponse>(
                    "xml_club_b.php",
                    new Dictionary<string, object>() { { "ville", city } }
                ))
                .Clubs ?? throw ApiException.NoResult();
        }

        /// <summary>
        /// Returns the details for a club.
        /// </summary>
        /// <param name="clubNumber">Club's number</param>
        /// <param name="teamId">[Optional] Team's identifier: if filled in, the room returned is the team room, otherwise the main club room</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<ClubDetails> GetClubDetailsAsync(string clubNumber, int teamId = 0)
        {
            return
                (await ApiGetAsync<ClubDetailsResponse>(
                    "xml_club_detail.php",
                    new Dictionary<string, object>() { { "club", clubNumber }, { "idequipe", teamId } }
                ))
                .ClubDetails ?? throw ApiException.NoResult();
        }

        #endregion clubs

        #region organizations

        /// <summary>
        /// Returns a list of organizations.
        /// </summary>
        /// <param name="type">Type of organization (F=Fédération (Federation), Z=Zone (Area), L=Ligue (League), D=Département (Department))</param>
        /// <param name="parentId">[Optional] Returns only those organisations whose parent is specified</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<Organization>> GetOrganizationsAsync(char type, int parentId = 0)
        {
            return 
                (await ApiGetAsync<OrganizationsResponse>(
                    "xml_organisme.php",
                    new Dictionary<string, object>() { { "type", type }, { "pere", parentId } }
                ))
                .Organizations ?? throw ApiException.NoResult();
        }

        #endregion organizations

        #region events type

        /// <summary>
        /// Returns a list of events for an organization.
        /// </summary>
        /// <param name="organizationId">unique ID of the organization</param>
        /// <param name="eventType">Event's Type (E= Equipes (Teams), I=Individuelles (Individuals))</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<EventType>> GetEventsTypeAsync(int organizationId, char eventType)
        {
            return 
                (await ApiGetAsync<EventsTypeResponse>(
                    "xml_epreuve.php",
                    new Dictionary<string, object>() { { "organisme", organizationId }, { "type", eventType } }
                ))
                .EventsType ?? throw ApiException.NoResult();
        }

        #endregion events type

        #region divisions

        /// <summary>
        /// Returns a list of divisions for a given event.
        /// </summary>
        /// <param name="organizationId">Organisation's identifier</param>
        /// <param name="eventId">Event's identifier</param>
        /// <param name="eventType">Event's Type (E= Equipes (Teams), I=Individuelles (Individuals))</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<Division>> GetDivisionsAsync(int organizationId, int eventId, char eventType)
        {
            return 
                (await ApiGetAsync<DivisionsResponse>(
                    "xml_division.php",
                    new Dictionary<string, object>() {
                        { "organisme", organizationId },
                        { "epreuve", eventId },
                        { "type", eventType }
                    }
                ))
                .Divisions ?? throw ApiException.NoResult();
        }

        #endregion divisions

        #region groups

        /// <summary>
        /// Returns the list of groups in the division.
        /// </summary>
        /// <param name="divisionId">Division's id</param>
        /// <param name="groupId">[Optional] Group's id: if omitted, positioned on the first group</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<Group>> GetGroupsAsync(int divisionId, int groupId = 0)
        {
            return 
                (await ApiGetAsync<GroupsResponse>(
                    "xml_result_equ.php",
                    new Dictionary<string, object>() { { "action", "poule" }, { "D1", divisionId }, { "cx_poule", groupId } }
                ))
                .Groups ?? throw ApiException.NoResult();
        }

        /// <summary>
        /// Returns the ranking of a team championship group.
        /// </summary>
        /// <param name="divisionId">Division's id</param>
        /// <param name="groupId">[Optional] Group's id: if omitted, positioned on the first group</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<GroupRanking>> GetGroupRankingAsync(int divisionId, int groupId = 0)
        {
            return 
                (await ApiGetAsync<GroupRankingResponse>(
                    "xml_result_equ.php",
                    new Dictionary<string, object>() { { "action", "classement" }, { "D1", divisionId }, { "cx_poule", groupId } }
                ))
                .GroupsRanking ?? throw ApiException.NoResult();
        }

        /// <summary>
        /// Returns the initial ranking of a team championship group.
        /// </summary>
        /// <param name="divisionId">Division's id</param>
        /// <param name="groupId">[Optional] Group's id: if omitted, positioned on the first group</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<GroupRanking>> GetInitialGroupRankingAsync(int divisionId, int groupId = 0)
        {
            return 
                (await ApiGetAsync<GroupRankingResponse>(
                    "xml_result_equ.php",
                    new Dictionary<string, object>() { { "action", "initial" }, { "D1", divisionId }, { "cx_poule", groupId } }
                ))
                .GroupsRanking ?? throw ApiException.NoResult();
        }

        #endregion groups

        #region games

        /// <summary>
        /// Returns the results of a team championship group.
        /// </summary>
        /// <param name="divisionId">Division's id</param>
        /// <param name="groupId">[Optional] Group's id: if omitted, positioned on the first group</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<GameResult>> GetGamesResultAsync(int divisionId, int groupId = 0)
        {
            return 
                (await ApiGetAsync<GamesResultResponse>(
                    "xml_result_equ.php",
                    new Dictionary<string, object>() { { "D1", divisionId }, { "cx_poule", groupId } }
                ))
                .GamesResult ?? throw ApiException.NoResult();
        }

        /// <summary>
        /// Returns the results of one or more team championship groups.
        /// </summary>
        /// <param name="groupsId">Contains the list of group ids for which we want to retrieve the results</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<GameResult>> GetGamesResultAsync(params int[] groupsId)
        {
            return 
                (await ApiGetAsync<GamesResultResponse>(
                    "xml_rencontre_equ.php",
                    new Dictionary<string, object>() { { "poule", string.Join('|', groupsId) } }
                ))
                .GamesResult ?? throw ApiException.NoResult();
        }

        /// <summary>
        /// Returns the detailed information of a game.
        /// </summary>
        /// <param name="parameters">These parameters are returned by the "GetGamesResultAsync" method in the "Link" property (GameParameters.Create(Link)).</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<GameInformations> GetGameInformationsAsync(GameParameters parameters)
        {
            var informations = await ApiGetAsync<GameInformations>("xml_chp_renc.php", parameters.GetQueryParameters());
            
            return informations.Result != null ? informations : throw ApiException.NoResult();
        }

        #endregion games

        #region teams

        /// <summary>
        /// Returns a list of a club's teams.
        /// </summary>
        /// <param name="clubNumber">Club's number</param>
        /// <param name="type">[Optional] M for teams in the French Men's Championship, F for teams in the French Women's Championship, 
        /// A for Men's and Women's teams in the French championship, nothing for all other teams</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<Team>> GetTeamsAsync(string clubNumber, char type = default)
        {
            return 
                (await ApiGetAsync<TeamsResponse>(
                    "xml_equipe.php",
                    new Dictionary<string, object>() { { "numclu", clubNumber }, { "type", type } }
                ))
                .Teams ?? throw ApiException.NoResult();
        }

        #endregion teams

        //TO DO xml_result_indiv => Currently not working
        //TO DO xml_res_cla => Currently not working

        #region players

        /// <summary>
        /// Returns a player from the spid database based on his license number.
        /// </summary>
        /// <param name="licenceNumber">Player's licence number</param>
        /// <param name="isValid">[Optional] By default all players (isValid = false). If isValid = true only validated players are returned.</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<Player> GetPlayerByLicenceAsync(int licenceNumber, bool isValid = false)
        {
            return 
                (await ApiGetAsync<PlayersResponse>(
                    "xml_liste_joueur_o.php",
                    new Dictionary<string, object>() { { "licence", licenceNumber }, { "valid", isValid } }
                ))
                .Players?.First() ?? throw ApiException.NoResult();
        }

        /// <summary>
        /// Returns players from the spid database based on a club number.
        /// </summary>
        /// <param name="clubNumber">Club's number</param>
        /// <param name="lastname">[Optional] Player's lastname (it does not have to be complete) </param>
        /// <param name="firstname">[Optional] Player's firstname</param>
        /// <param name="isValid">[Optional] By default all players (isValid = false). If isValid = true only validated players are returned.</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<Player>> GetPlayersByClubAsync(string clubNumber, string lastname = "", string firstname = "", bool isValid = false)
        {
            return 
                (await ApiGetAsync<PlayersResponse>(
                    "xml_liste_joueur_o.php",
                    new Dictionary<string, object>() { { "club", clubNumber }, { "nom", lastname }, { "prenom", firstname }, { "valid", isValid } }
                ))
                .Players ?? throw ApiException.NoResult();
        }

        /// <summary>
        /// Returns players from the spid database based on a name (full or begins with).
        /// </summary>
        /// <param name="lastname">Player's lastname (it does not have to be complete) </param>
        /// <param name="firstname">[Optional] Player's firstname</param>
        /// <param name="isValid">[Optional] By default all players (isValid = false). If isValid = true only validated players are returned.</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<Player>> GetPlayersByNameAsync(string lastname, string firstname = "", bool isValid = false)
        {
            return 
                (await ApiGetAsync<PlayersResponse>(
                    "xml_liste_joueur_o.php",
                    new Dictionary<string, object>() { { "nom", lastname }, { "prenom", firstname }, { "valid", isValid } }
                ))
                .Players ?? throw ApiException.NoResult();
        }

        /// <summary>
        /// Returns a list of players from the ranking database based on a club number.
        /// </summary>
        /// <param name="clubNumber">Club's number</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<PlayerRankingLight>> GetPlayersRankingByClubAsync(string clubNumber)
        {
            return 
                (await ApiGetAsync<PlayersRankingLightResponse>(
                    "xml_liste_joueur.php",
                    new Dictionary<string, object>() { { "club", clubNumber } }
                ))
                .PlayersRanking ?? throw ApiException.NoResult();
        }

        /// <summary>
        /// Returns a list of players from the ranking database based on a name (full or begins with).
        /// </summary>
        /// <param name="lastname">Player's lastname (it does not have to be complete) </param>
        /// <param name="firstname">[Optional] Player's firstname (it does not have to be complete)</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<PlayerRankingLight>> GetPlayersRankingByNameAsync(string lastname, string firstname = "")
        {
            return 
                (await ApiGetAsync<PlayersRankingLightResponse>(
                    "xml_liste_joueur.php",
                    new Dictionary<string, object>() { { "nom", lastname }, { "prenom", firstname } }
                ))
                .PlayersRanking ?? throw ApiException.NoResult();
        }

        /// <summary>
        /// Returns a player's ranking (full information).
        /// </summary>
        /// <param name="licenceNumber">Player's licence number</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<PlayerRanking> GetPlayerRankingAsync(int licenceNumber)
        {
            return 
                (await ApiGetAsync<PlayerRankingResponse>(
                    "xml_joueur.php",
                    new Dictionary<string, object>() { { "licence", licenceNumber } }
                ))
                .PlayerRanking ?? throw ApiException.NoResult();
        }

        /// <summary>
        /// Returns a player's licence from the SPID database.
        /// </summary>
        /// <param name="licenceNumber">Player's licence number</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<PlayerLicence> GetPlayerLicenceAsync(int licenceNumber)
        {
            return
                (await ApiGetAsync<PlayerLicenceResponse>(
                    "xml_licence.php",
                    new Dictionary<string, object>() { { "licence", licenceNumber } }
                ))
                .PlayerLicence ?? throw ApiException.NoResult();
        }

        /// <summary>
        /// Returns a player's licence from the SPID database and information on its monthly progress.
        /// </summary>
        /// <param name="licenceNumber">Player's licence number</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<PlayerLicence> GetPlayerLicenceAndMonthlyProgressAsync(int licenceNumber)
        {
            return 
                (await ApiGetAsync<PlayerLicenceResponse>(
                    "xml_licence_b.php",
                    new Dictionary<string, object>() { { "licence", licenceNumber } }
                ))
                .PlayerLicence ?? throw ApiException.NoResult();
        }

        /// <summary>
        /// Returns a list of a player's games from the MySql database.
        /// </summary>
        /// <param name="licenceNumber">Player's licence number</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<PlayerGameMySql>> GetPlayerGamesMySqlAsync(int licenceNumber)
        {
            return 
                (await ApiGetAsync<PlayerGamesMySqlResponse>(
                    "xml_partie_mysql.php",
                    new Dictionary<string, object>() { { "licence", licenceNumber } }
                ))
                .PlayerGamesMySql ?? throw ApiException.NoResult();
        }

        /// <summary>
        /// Returns a list of a player's games from the SPID database.
        /// </summary>
        /// <param name="licenceNumber">Player's licence number</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<PlayerGameSpid>> GetPlayerGamesSpidAsync(int licenceNumber)
        {
            return 
                (await ApiGetAsync<PlayerGamesSpidResponse>(
                    "xml_partie.php",
                    new Dictionary<string, object>() { { "numlic", licenceNumber } }
                ))
                .PlayerGamesSpid ?? throw ApiException.NoResult();
        }

        /// <summary>
        /// Returns the player's ranking history.
        /// </summary>
        /// <param name="licenceNumber">Player's licence number</param>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<PlayerHistory>> GetPlayerHistoryAsync(int licenceNumber)
        {
            return
                (await ApiGetAsync<PlayerHistoryResponse>(
                    "xml_histo_classement.php",
                    new Dictionary<string, object>() { { "numlic", licenceNumber } }
                ))
                .PlayerHistory ?? throw ApiException.NoResult();
        }

        #endregion players

        #region news

        /// <summary>
        /// Returns the FFTT news feed.
        /// </summary>
        /// <exception cref="ApiException">Thrown when an unknown error has occurred.</exception>
        /// <exception cref="ApiNotFoundException">Thrown when no results are found.</exception>
        /// <exception cref="ApiInvalidParametersException">Thrown when a required parameter is empty or incorrect.</exception>
        /// <exception cref="ApiUnauthorizedException">Thrown when the identification parameters (serie, tm, tmc and id) are missing or incorrect.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<News>> GetNewsAsync()
        {
            return (await ApiGetAsync<NewsResponse>("xml_new_actu.php")).News ?? throw ApiException.NoResult();
        }

        #endregion news

        #endregion public methods

        #region implementation IDisposable

        public void Dispose()
        { }

        #endregion implementation IDisposable
    }
}