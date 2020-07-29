using GameBox.Admin.UI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Services.Contracts
{
    public interface IGameService
    {
        Task<IEnumerable<GamesListModel>> SearchGames(string title = "");

        Task<GameFormModel> GetGame(string gameId);

        Task AddGame(GameFormModel game);

        Task EditGame(string gameId, GameFormModel game);
        
        Task DeleteGame(string gameId);
    }
}