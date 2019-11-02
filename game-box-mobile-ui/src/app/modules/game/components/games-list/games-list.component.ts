import { Component, OnInit } from '@angular/core';

import { forkJoin } from 'rxjs';

import { IGamesListModel } from '../../models/games-list.model';
import { GameService } from '../../services/game.service';

@Component({
  selector: 'app-games-list',
  templateUrl: './games-list.component.html',
  styleUrls: ['./games-list.component.scss'],
  moduleId: module.id
})
export class GamesListComponent implements OnInit {
  public games: IGamesListModel[] = [];
  public owned: IGamesListModel[] = [];

  constructor(private gameService: GameService) { }

  public ngOnInit(): void {
    forkJoin(
      this.gameService.getGames$(this.games.length),
      this.gameService.getGames$(this.games.length) // change to getOwned$
    )
    .subscribe(([gamesResult, ownedResult]: [IGamesListModel[], IGamesListModel[]]) => {
      this.games = [...this.games, ...gamesResult];
      this.owned = [...this.owned, ...ownedResult];
    });
  }
}
