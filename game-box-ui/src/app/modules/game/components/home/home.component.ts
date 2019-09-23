import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';

import { IGamesHomeListModel } from '../../models/games-home-list.model';
import { IAppState } from 'src/app/store/app.state';
import { GameService } from '../../services/game.service';
import { tap } from 'rxjs/operators';
import { ClearGames } from 'src/app/store/games/games.actions';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit, OnDestroy {
  private gamesLength = 0;

  public games$: Observable<IGamesHomeListModel[]>;

  constructor(
    private gameService: GameService,
    private store: Store<IAppState>
  ) { }

  public ngOnInit(): void {
    this.loadGames();
  }

  public ngOnDestroy(): void {
    this.store.dispatch(new ClearGames('home'));
  }

  public loadGames(): void {
    this.gameService
      .getGames$(this.gamesLength)
      .subscribe(() => {
        this.games$ = this.store.pipe(
          select(state => state.games.allHome),
          tap(games => this.gamesLength = games.length)
        );
      });
  }
}
