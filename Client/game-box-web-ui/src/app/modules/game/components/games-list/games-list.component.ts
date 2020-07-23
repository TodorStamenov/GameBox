import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';

import { IGamesListModel } from '../../models/games-list.model';
import { LoadAllGames } from 'src/app/modules/game/+store/games.actions';
import { IState } from '../../+store/games.state';

@Component({
  selector: 'app-games-list',
  templateUrl: './games-list.component.html'
})
export class GamesListComponent implements OnInit {
  public games$: Observable<IGamesListModel[]>;

  constructor(private store: Store<IState>) { }

  public ngOnInit(): void {
    this.searchGames('');
    this.games$ = this.store.pipe(
      select(s => s.games.all)
    );
  }

  public searchGames(title: string): void {
    this.store.dispatch(new LoadAllGames(title));
  }
}
