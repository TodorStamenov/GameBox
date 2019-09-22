import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';

import { IGamesHomeListModel } from '../../models/games/games-list.model';
import { GameService } from '../../services/game.service';
import { IAppState } from 'src/app/store/app.state';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
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
      .getGames$(this.games.length, '')
      .subscribe(() => {
        this.store.pipe(
          select(state => state.games.byCategory)
        )
        .subscribe((res: IGamesHomeListModel[]) => this.games = [...this.games, ...res]);
      });
  }
}
