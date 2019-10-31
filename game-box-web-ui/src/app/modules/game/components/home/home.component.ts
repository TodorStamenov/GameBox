import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { IAppState } from 'src/app/store/app.state';
import { IGamesHomeListModel } from '../../models/games-home-list.model';
import { LoadAllGamesHome, ClearGamesHome } from 'src/app/store/games/games.actions';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit, OnDestroy {
  private gamesLength = 0;

  public games$: Observable<IGamesHomeListModel[]>;

  constructor(private store: Store<IAppState>) { }

  public ngOnInit(): void {
    this.loadGames();
    this.games$ = this.store.pipe(
      select(s => s.games.allHome),
      tap(games => this.gamesLength = games.length)
    );
  }

  public ngOnDestroy(): void {
    this.store.dispatch(new ClearGamesHome());
  }

  public loadGames(): void {
    this.store.dispatch(new LoadAllGamesHome(this.gamesLength));
  }
}
