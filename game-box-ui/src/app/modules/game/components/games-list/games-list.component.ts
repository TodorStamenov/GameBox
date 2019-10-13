import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';

import { IAppState } from 'src/app/store/app.state';
import { IGamesListModel } from '../../models/games-list.model';
import { LoadAllGames } from 'src/app/store/games/games.actions';

@Component({
  selector: 'app-games-list',
  templateUrl: './games-list.component.html',
  styleUrls: ['./games-list.component.scss']
})
export class GamesListComponent implements OnInit {
  public games$: Observable<IGamesListModel[]>;

  constructor(private store: Store<IAppState>) { }

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
