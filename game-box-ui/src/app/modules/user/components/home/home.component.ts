import { Component, OnInit } from '@angular/core';
import { ListGamesModel } from '../../models/list-games.model';
import { GameService } from '../../services/game.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
  public games: ListGamesModel[] = [];

  constructor(private gameService: GameService) { }

  ngOnInit(): void {
    this.loadGames();
  }

  loadGames(): void {
    this.gameService
      .getGames(this.games.length, '')
      .subscribe(res => this.games = [...this.games, ...res]);
  }
}