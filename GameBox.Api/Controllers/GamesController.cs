using GameBox.Services.Contracts;

namespace GameBox.Api.Controllers
{
    public class GamesController : BaseApiController
    {
        private readonly IGameService game;

        public GamesController(IGameService game)
        {
            this.game = game;
        }
    }
}