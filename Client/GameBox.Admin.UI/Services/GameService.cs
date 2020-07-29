using GameBox.Admin.UI.Model;
using GameBox.Admin.UI.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Services
{
    public class GameService : IGameService
    {
        private readonly string gamesUrl;
        private readonly IHttpClientService httpClient;

        public GameService(
            IHttpClientService httpClient,
            ConfigurationSettings config)
        {
            this.httpClient = httpClient;
            this.gamesUrl = $"{config.GameBoxApiUrl}games/";
        }

        public async Task<IEnumerable<GamesListModel>> SearchGames(string title = "")
        {
            return await this.httpClient.GetAsync<IEnumerable<GamesListModel>>($"{this.gamesUrl}all?title={title}");
        }

        public async Task<GameFormModel> GetGame(string gameId)
        {
            return await this.httpClient.GetAsync<GameFormModel>(this.gamesUrl + gameId);
        }

        public async Task AddGame(GameFormModel game)
        {
            await this.httpClient.PostAsync<string>(this.gamesUrl, game);
        }

        public async Task EditGame(string gameId, GameFormModel game)
        {
            await this.httpClient.PutAsync<string>(this.gamesUrl + gameId, game);
        }

        public async Task DeleteGame(string gameId)
        {
            await this.httpClient.DeleteAsync<string>(this.gamesUrl + gameId);
        }
    }
}