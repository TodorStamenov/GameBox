import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';

import { IGamesHomeListModel } from '../../../models/games/games-list.model';
import { GameService } from '../../../services/game.service';
import { IAppState } from 'src/app/store/app.state';

@Component({
  selector: 'app-owned-games',
  templateUrl: './owned-games.component.html'
})
export class OwnedGamesComponent implements OnInit {
  public games: IGamesHomeListModel[] = [];

  constructor(
    private gameService: GameService,
    private store: Store<IAppState>
  ) { }

  public ngOnInit(): void {
    this.loadGames();
  }

  public loadGames(): void {
    this.gameService
      .getOwned$(this.games.length)
      .subscribe(() => {
        this.store.pipe(
          select(state => state.games.owned)
        )
        .subscribe((res: IGamesHomeListModel[]) => this.games = [...this.games, ...res]);
      });
  }
}
