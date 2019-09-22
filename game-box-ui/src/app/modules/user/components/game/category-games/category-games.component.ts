import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';

import { GameService } from '../../../services/game.service';
import { IGamesHomeListModel } from '../../../models/games/games-list.model';
import { IAppState } from 'src/app/store/app.state';

@Component({
  selector: 'app-category-games',
  templateUrl: './category-games.component.html'
})
export class CategoryGamesComponent implements OnInit {
  public categoryId: string;
  public games: IGamesHomeListModel[] = [];

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

  public loadGames(): void {
    this.gameService
      .getGames$(this.games.length, this.categoryId)
      .subscribe(() => {
        this.store.pipe(
          select(state => state.games.byCategory)
        )
        .subscribe((res: IGamesHomeListModel[]) => this.games = [...this.games, ...res]);
      });
  }
}
