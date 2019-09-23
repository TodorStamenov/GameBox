import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { IAppState } from 'src/app/store/app.state';
import { IGamesHomeListModel } from '../../models/games-home-list.model';
import { GameService } from '../../services/game.service';
import { ClearGames } from 'src/app/store/games/games.actions';

@Component({
  selector: 'app-owned-games',
  templateUrl: './owned-games.component.html'
})
export class OwnedGamesComponent implements OnInit, OnDestroy {
  private gamesLength = 1;

  public games$: Observable<IGamesHomeListModel[]>;

  constructor(
    private gameService: GameService,
    private store: Store<IAppState>
  ) { }

  public ngOnInit(): void {
    this.loadGames();
  }

  public ngOnDestroy(): void {
    this.store.dispatch(new ClearGames('owned'));
  }

  public loadGames(): void {
    this.gameService
      .getOwned$(this.gamesLength)
      .subscribe(() => {
        this.games$ = this.store.pipe(
          select(state => state.games.owned),
          tap(games => this.gamesLength = games.length)
        );
      });
  }
}
