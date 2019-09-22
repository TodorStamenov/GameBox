import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';

import { GameService } from '../../../services/game.service';
import { IAppState } from 'src/app/store/app.state';
import { IGamesListModel } from '../../../models/games/games-list.model';

@Component({
  selector: 'app-games-list',
  templateUrl: './games-list.component.html',
  styleUrls: ['./games-list.component.css']
})
export class GamesListComponent implements OnInit {
  public games$: Observable<IGamesListModel[]>;

  constructor(
    private gameService: GameService,
    private store: Store<IAppState>
  ) { }

  public ngOnInit(): void {
    this.gameService
      .getGames$()
      .subscribe(() => {
        this.games$ = this.store.pipe(
          select(state => state.games.all)
        );
      });
  }

  public searchGames(title: string): void {
    this.gameService
      .getGames$(title || '')
      .subscribe(() => {
        this.games$ = this.store.pipe(
          select(state => state.games.all)
        );
      });
  }
}
