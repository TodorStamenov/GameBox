import { Component, OnInit } from '@angular/core';

import { IListGamesModel } from '../../models/list-games.model';
import { GameService } from '../../services/game.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
  public games: IListGamesModel[] = [];

  constructor(private gameService: GameService) { }

  public ngOnInit(): void {
    this.loadGames();
  }

  public loadGames(): void {
    this.gameService
      .getGames$(this.games.length, '')
      .subscribe(res => this.games = [...this.games, ...res]);
  }
}
