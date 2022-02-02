using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using FG.FFTT.Api.Client.Models;
using FG.FFTT.Api.Client.Extensions;
using FG.FFTT.Api.Client.Exceptions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FG.FFTT.Api.Client.UnitTests
{
    /// <summary>
    /// Unit tests to validate calls to the FFTT API from the developed client.
    /// They also validate the deserialization of the different models used in the library.
    /// </summary>
    [TestClass]
    public class FfttClientTests
    {
        #region private fields

        FfttCredentials _credentials = new FfttCredentials("XXXXX", "XXXXXXXXXX", "XXXXXXXXXXXXXXX");

        #endregion private fields

        #region public methods

        #region user initialisation

        [TestMethod]
        public async Task UserInitializationAsync_ShouldReturnValidObject()
        {
            UserInitialization response;
            using (var client = new FfttClient(_credentials))
                response = await client.UserInitializationAsync();

            // The result must return a valid account
            Assert.IsTrue(response.IsAuthorized);
        }

        #endregion user initialisation

        #region club

        [TestMethod]
        public async Task GetClubByNumberAsync_ShouldReturnValidObject()
        {
            Club club;
            using (var client = new FfttClient(_credentials))
                club = await client.GetClubByNumberAsync("08781094");

            // The result must match this club name
            Assert.AreEqual(club.Name, "MONTFORT TT US");
        }

        [TestMethod]
        public async Task GetClubByPostalCodeAsync_ShouldReturnValidObject()
        {
            Club club;
            using (var client = new FfttClient(_credentials))
                club = await client.GetClubByPostalCodeAsync(78490);

            // The result must match this club name
            Assert.AreEqual(club.Name, "MONTFORT TT US");
        }

        [TestMethod]
        public async Task GetClubsByDepartmentAsync_ShouldReturnValidObject()
        {
            IEnumerable<Club> clubs;
            using (var client = new FfttClient(_credentials))
                clubs = await client.GetClubsByDepartmentAsync(78);

            // The result must return at least one answer
            Assert.IsTrue(clubs.Count() > 0);

            // The result must contain this club
            Assert.IsTrue(clubs.Any(c => c.Id == 12781094));
        }

        [TestMethod]
        public async Task GetClubsByCityAsync_ShouldReturnValidObject()
        {
            IEnumerable<Club> clubs;
            using (var client = new FfttClient(_credentials))
                clubs = await client.GetClubsByCityAsync("MONTFORT");

            // The result must return at least one answer
            Assert.IsTrue(clubs.Count() > 0);

            // The result must contain this club
            Assert.IsTrue(clubs.Any(c => c.Id == 12781094));
        }

        [TestMethod]
        public async Task GetClubDetailsAsync_ShouldReturnValidObject()
        {
            ClubDetails details;
            using (var client = new FfttClient(_credentials))
                details = await client.GetClubDetailsAsync("08781094");

            // The result must correspond to the Montfort club
            Assert.IsTrue(details.Name == "MONTFORT TT US");
        }

        [TestMethod]
        [ExpectedException(typeof(ApiNotFoundException))]
        public async Task GetClubDetailsAsync_ShouldReturnNoResultException()
        {
            using (var client = new FfttClient(_credentials))
                await client.GetClubDetailsAsync("99999999");
        }

        #endregion club

        #region organizations

        [TestMethod]
        public async Task GetOrganizationsAsync_ShouldReturnValidObject()
        {
            IEnumerable<Organization> organizations;
            using (var client = new FfttClient(_credentials))
                organizations = await client.GetOrganizationsAsync('D');

            // The result must return at least one answer
            Assert.IsTrue(organizations.Count() > 0);

            // The result must contain this organization
            Assert.IsTrue(organizations.Any(o => o.Label == "YVELINES"));
        }

        #endregion organizations

        #region events type

        [TestMethod]
        public async Task GetEventsTypeAsync_ShouldReturnValidObject()
        {
            IEnumerable<EventType> eventsType;
            using (var client = new FfttClient(_credentials))
                eventsType = await client.GetEventsTypeAsync(82, 'E');

            // The result must return at least one answer
            Assert.IsNotNull(eventsType);
        }

        #endregion events type

        #region divisions

        [TestMethod]
        public async Task GetDivisionsAsync_ShouldReturnValidObject()
        {
            IEnumerable<Division> divisions;
            using (var client = new FfttClient(_credentials))
                divisions = await client.GetDivisionsAsync(82, 2302, 'I');

            // The result must return at least one answer
            Assert.IsNotNull(divisions);
        }

        #endregion divisions

        #region groups

        [TestMethod]
        public async Task GetGroupsAsync_ShouldReturnValidObject()
        {
            IEnumerable<Group> groups;
            using (var client = new FfttClient(_credentials))
                groups = await client.GetGroupsAsync(37274);

            // The result must return at least one answer
            Assert.IsNotNull(groups);
        }

        [TestMethod]
        public async Task GetGroupRankingAsync_ShouldReturnValidObject()
        {
            IEnumerable<GroupRanking> groupRanking;
            using (var client = new FfttClient(_credentials))
                groupRanking = await client.GetGroupRankingAsync(37274);

            // The result must return at least one answer
            Assert.IsNotNull(groupRanking);
        }

        [TestMethod]
        public async Task GetInitialGroupRankingAsync_ShouldReturnValidObject()
        {
            IEnumerable<GroupRanking> groupRanking;
            using (var client = new FfttClient(_credentials))
                groupRanking = await client.GetInitialGroupRankingAsync(37274);

            // The result must return at least one answer
            Assert.IsNotNull(groupRanking);
        }

        #endregion groups

        #region games

        [TestMethod]
        public async Task GetGamesResultAsync_ShouldReturnValidObject()
        {
            IEnumerable<GameResult> result;
            using (var client = new FfttClient(_credentials))
                result = await client.GetGamesResultAsync(new int[] { 154813, 154815 });

            // The result must return at least one answer
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetGameInformationsAsync_ShouldReturnValidObject()
        {
            string link = "renc_id=606110&is_retour=0&phase=2&res_1=25&res_2=17&equip_1=MONTFORT+TT+US+1&equip_2=SQY+PING+10&equip_id1=8002&equip_id2=9107";

            GameInformations informations;
            using (var client = new FfttClient(_credentials))
                informations = await client.GetGameInformationsAsync(link.ToObject<GameParameters>());

            // The result must return at least one answer
            Assert.IsNotNull(informations.Players);
            Assert.IsNotNull(informations.Matches);
            Assert.IsNotNull(informations.Result);
        }

        #endregion games

        #region teams

        [TestMethod]
        public async Task GetTeamsAsync_ShouldReturnValidObject()
        {
            IEnumerable<Team> teams;
            using (var client = new FfttClient(_credentials))
                teams = await client.GetTeamsAsync("08781094", 'A');

            // The result must return at least one answer
            Assert.IsNotNull(teams);
        }

        #endregion teams

        #region players

        [TestMethod]
        public async Task GetPlayerByLicenceAsync_ShouldReturnValidObject()
        {
            Player player;
            using (var client = new FfttClient(_credentials))
                player = await client.GetPlayerByLicenceAsync(7834155);

            // The result must match this player name
            Assert.AreEqual(player.LastName, "GOLDSCHMITT");
        }

        [TestMethod]
        public async Task GetPlayersByClubAsync_ShouldReturnValidObject()
        {
            IEnumerable<Player> players;
            using (var client = new FfttClient(_credentials))
                players = await client.GetPlayersByClubAsync("08781094");

            // The result must return at least one answer
            Assert.IsTrue(players.Count() > 0);

            // The result must contain this player
            Assert.IsTrue(players.Any(p => p.LastName == "GOLDSCHMITT"));
        }

        [TestMethod]
        public async Task GetPlayersByNameAsync_ShouldReturnValidObject()
        {
            IEnumerable<Player> players;
            using (var client = new FfttClient(_credentials))
                players = await client.GetPlayersByNameAsync("gol");

            // The result must return at least one answer
            Assert.IsTrue(players.Count() > 0);

            // The result must contain this player
            Assert.IsTrue(players.Any(p => p.LastName == "GOLDSCHMITT"));
        }

        [TestMethod]
        public async Task GetPlayersRankingByClubAsync_ShouldReturnValidObject()
        {
            IEnumerable<PlayerRankingLight> playersRanking;
            using (var client = new FfttClient(_credentials))
                playersRanking = await client.GetPlayersRankingByClubAsync("08781094");

            // The result must return at least one answer
            Assert.IsTrue(playersRanking.Count() > 0);

            // The result must contain this player
            Assert.IsTrue(playersRanking.Any(p => p.LastName == "GOLDSCHMITT"));
        }

        [TestMethod]
        public async Task GetPlayersRankingByNameAsync_ShouldReturnValidObject()
        {
            IEnumerable<PlayerRankingLight> playersRanking;
            using (var client = new FfttClient(_credentials))
                playersRanking = await client.GetPlayersRankingByNameAsync("GOL");

            // The result must return at least one answer
            Assert.IsTrue(playersRanking.Count() > 0);

            // The result must contain this player
            Assert.IsTrue(playersRanking.Any(p => p.LastName == "GOLDSCHMITT"));
        }

        [TestMethod]
        public async Task GetPlayerRankingAsync_ShouldReturnValidObject()
        {
            PlayerRanking playerRanking;
            using (var client = new FfttClient(_credentials))
                playerRanking = await client.GetPlayerRankingAsync(7834155);

            // The result must contain this player
            Assert.IsTrue(playerRanking.LastName == "GOLDSCHMITT");
        }

        [TestMethod]
        public async Task GetPlayerLicenceAsync_ShouldReturnValidObject()
        {
            PlayerLicence licence;
            using (var client = new FfttClient(_credentials))
                licence = await client.GetPlayerLicenceAsync(7834155);

            // The result must contain this player
            Assert.IsTrue(licence.Lastname == "GOLDSCHMITT");
        }

        [TestMethod]
        public async Task GetPlayerLicenceAndMonthlyProgressAsync_ShouldReturnValidObject()
        {
            PlayerLicence licence;
            using (var client = new FfttClient(_credentials))
                licence = await client.GetPlayerLicenceAndMonthlyProgressAsync(7834155);

            // The result must contain this player
            Assert.IsTrue(licence.Lastname == "GOLDSCHMITT");
        }

        [TestMethod]
        public async Task GetPlayerGamesMySqlAsync_ShouldReturnValidObject()
        {
            IEnumerable<PlayerGameMySql> playerGames;
            using (var client = new FfttClient(_credentials))
                playerGames = await client.GetPlayerGamesMySqlAsync(7834155);

            // The result must return at least one answer
            Assert.IsTrue(playerGames.Count() > 0);

            // The result must contain this player
            Assert.IsTrue(playerGames.Any(p => p.PlayerLicence == 7834155));
        }

        [TestMethod]
        public async Task GetPlayerGamesSpidAsync_ShouldReturnValidObject()
        {
            IEnumerable<PlayerGameSpid> playerGames;
            using (var client = new FfttClient(_credentials))
                playerGames = await client.GetPlayerGamesSpidAsync(7834155);

            // The result must return at least one answer
            Assert.IsNotNull(playerGames);
        }

        [TestMethod]
        public async Task GetPlayerHistoryAsync_ShouldReturnValidObject()
        {
            IEnumerable<PlayerHistory> history;
            using (var client = new FfttClient(_credentials))
                history = await client.GetPlayerHistoryAsync(153466);

            // The result must return at least one answer
            Assert.IsNotNull(history);
        }

        #endregion players

        #region news

        [TestMethod]
        public async Task GetNewsAsync_ShouldReturnValidObject()
        {
            IEnumerable<News> news;
            using (var client = new FfttClient(_credentials))
                news = await client.GetNewsAsync();

            // The result must return at least one answer
            Assert.IsNotNull(news);
        }

        #endregion news

        #endregion public methods
    }
}