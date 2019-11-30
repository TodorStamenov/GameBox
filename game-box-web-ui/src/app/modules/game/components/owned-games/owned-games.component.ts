import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { IGamesHomeListModel } from '../../models/games-home-list.model';
import { LoadAllOwnedGames, ClearOwnedGames } from 'src/app/modules/game/+store/games.actions';
import { IState } from '../../+store/games.state';

@Component({
  selector: 'app-owned-games',
  templateUrl: './owned-games.component.html'
})
export class OwnedGamesComponent implements OnInit, OnDestroy {
  private gamesLength = 0;

  public games$: Observable<IGamesHomeListModel[]>;

  constructor(private store: Store<IState>) { }

  public ngOnInit(): void {
    this.loadGames();
    this.games$ = this.store.pipe(
      select(s => s.games.owned),
      tap(games => this.gamesLength = games.length)
    );
  }

  public ngOnDestroy(): void {
    this.store.dispatch(new ClearOwnedGames());
  }

  public loadGames(): void {
    this.store.dispatch(new LoadAllOwnedGames(this.gamesLength));
  }
}
