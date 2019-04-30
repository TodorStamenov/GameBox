import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';

import { GameService } from '../../../services/game.service';
import { IGamesListModel } from '../../../models/games/games-list.model';

@Component({
  selector: 'app-category-games',
  templateUrl: './category-games.component.html'
})
export class CategoryGamesComponent implements OnInit {
  public categoryId: string;
  public games: IGamesListModel[] = [];

  constructor(
    private gameService: GameService,
    private route: ActivatedRoute
  ) {
    this.categoryId = this.route.snapshot.params['categoryId'];
  }

  public ngOnInit(): void {
    this.loadGames();
  }

  public loadGames(): void {
    this.gameService
      .getGames$(this.games.length, this.categoryId)
      .subscribe((res: IGamesListModel[]) => this.games = [...this.games, ...res]);
  }
}
