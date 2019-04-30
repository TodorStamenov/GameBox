import { Component, OnInit } from '@angular/core';

import { IGamesListModel } from '../../../models/games/games-list.model';
import { GameService } from '../../../services/game.service';

@Component({
  selector: 'app-owned-games',
  templateUrl: './owned-games.component.html'
})
export class OwnedGamesComponent implements OnInit {
  public games: IGamesListModel[] = [];

  constructor(private gameService: GameService) { }

  public ngOnInit(): void {
    this.loadGames();
  }

  public loadGames(): void {
    this.gameService
      .getOwned$(this.games.length)
      .subscribe((res: IGamesListModel[]) => this.games = [...this.games, ...res]);
  }
}
