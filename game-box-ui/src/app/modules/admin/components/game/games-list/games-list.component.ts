import { Component } from '@angular/core';

import { GameService } from '../../../services/game.service';

@Component({
  selector: 'app-games-list',
  templateUrl: './games-list.component.html',
  styleUrls: ['./games-list.component.css']
})
export class GamesListComponent {
  public games$ = this.gameService.getGames$('');

  constructor(private gameService: GameService) { }

  public searchGames(title: string): void {
    this.games$ = this.gameService.getGames$(title || '');
  }
}
