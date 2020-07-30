using GameBox.Admin.UI.Model;
using GameBox.Admin.UI.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Pages.DeleteGame
{
    [Route("/games/delete/{gameId}")]
    public partial class DeleteGame : ComponentBase
    {
        public GameFormModel gameForm = new GameFormModel();
        
        [Parameter] public string GameId { get; set; }

        [Inject] public IGameService GameService { get; set; }

        [Inject] public NavigationManager Router { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.gameForm = await this.GameService.GetGame(this.GameId);
        }

        public async Task OnDeleteGame()
        {
            await this.GameService.DeleteGame(this.GameId);
            this.Router.NavigateTo("/games/all");
        }
    }
}