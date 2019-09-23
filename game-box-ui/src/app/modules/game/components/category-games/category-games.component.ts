import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { IGamesHomeListModel } from '../../models/games-home-list.model';
import { IAppState } from 'src/app/store/app.state';
import { GameService } from '../../services/game.service';
import { ClearGames } from 'src/app/store/games/games.actions';

@Component({
  selector: 'app-category-games',
  templateUrl: './category-games.component.html'
})
export class CategoryGamesComponent implements OnInit, OnDestroy {
  private gamesLength = 0;

  public categoryId: string;
  public games$: Observable<IGamesHomeListModel[]>;

  constructor(
    private gameService: GameService,
    private route: ActivatedRoute,
    private store: Store<IAppState>
  ) {
    this.categoryId = this.route.snapshot.params['categoryId'];
  }

  public ngOnInit(): void {
    this.loadGames();
  }

  public ngOnDestroy(): void {
    this.store.dispatch(new ClearGames('category'));
  }

  public loadGames(): void {
    this.gameService
      .getGamesByCategory$(this.gamesLength, this.categoryId)
      .subscribe(() => {
        this.games$ = this.store.pipe(
          select(state => state.games.byCategory),
          tap(games => this.gamesLength = games.length)
        );
      });
  }
}
