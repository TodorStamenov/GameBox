using GameBox.Admin.UI.Model;
using GameBox.Admin.UI.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Pages.Games
{
    [Route("/games/all")]
    public partial class Games : ComponentBase
    {
        public IEnumerable<GamesListModel> games = new List<GamesListModel>();

        [Inject] public IGameService GameService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.games = await this.GameService.SearchGames();
        }

        public async Task SearchGames(string title)
        {
            this.games = await this.GameService.SearchGames(title);
        }
    }
}