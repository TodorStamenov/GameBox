import { Component, OnInit } from '@angular/core';
import { ListGamesModel } from '../../../models/list-games.model';
import { GameService } from '../../../services/game.service';

@Component({
  selector: 'app-owned-games',
  templateUrl: './owned-games.component.html'
})
export class OwnedGamesComponent implements OnInit {
  public games: ListGamesModel[] = [];

  constructor(private gameService: GameService) { }

  ngOnInit(): void {
    this.loadGames();
  }

  loadGames(): void {
    this.gameService
      .getOwned(this.games.length)
      .subscribe(res => this.games = [...this.games, ...res]);
  }
}