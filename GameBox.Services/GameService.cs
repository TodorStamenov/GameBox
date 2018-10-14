using GameBox.Data;
using GameBox.Services.Contracts;

namespace GameBox.Services
{
    public class GameService : Service, IGameService
    {
        public GameService(GameBoxDbContext database)
            : base(database)
        {
        }
    }
}