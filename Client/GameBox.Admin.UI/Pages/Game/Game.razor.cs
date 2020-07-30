using GameBox.Admin.UI.Model;
using GameBox.Admin.UI.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameBox.Admin.UI.Pages.Game
{
    [Route("/games/{action}")]
    public partial class Game : ComponentBase, IDisposable
    {
        private string gameId;
        public GameFormModel gameForm = new GameFormModel();
        public IEnumerable<CategoryMenuModel> categories = new List<CategoryMenuModel>();

        [Parameter] public string Action { get; set; }

        [Inject] public IGameService GameService { get; set; }

        [Inject] public ICategoryService CategoryService { get; set; }

        [Inject] public NavigationManager Router { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await this.PrepareView();
            this.Router.LocationChanged += ReRender;
        }

        public void Dispose()
        {
            this.Router.LocationChanged -= ReRender;
        }

        public async Task OnSaveGame()
        {
            if (this.Action.ToLower() == "edit" && !string.IsNullOrWhiteSpace(this.gameId))
            {
                await this.GameService.EditGame(this.gameId, this.gameForm);
            }
            else if (this.Action.ToLower() == "create")
            {
                await this.GameService.AddGame(this.gameForm);
            }

            this.Router.NavigateTo("/games/all");
        }

        private async Task PrepareView()
        {
            this.gameForm = new GameFormModel();
            this.gameId = this.Router.GetQueryParam("gameId");
            this.categories = await this.CategoryService.GetCategoryNames();

            if (this.Action.ToLower() == "edit" && !string.IsNullOrWhiteSpace(this.gameId))
            {
                this.gameForm = await this.GameService.GetGame(this.gameId);
            }

            this.StateHasChanged();
        }

        private void ReRender(object sender, LocationChangedEventArgs args)
        {
            Task.Run(this.PrepareView);
        }
    }
}