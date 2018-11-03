import { Component, Input } from '@angular/core';
import { ListGamesModel } from '../../../models/list-games.model';
import { AuthService } from 'src/app/modules/authentication/auth.service';

@Component({
  selector: 'app-game-card',
  templateUrl: './game-card.component.html'
})
export class GameCardComponent {
  @Input()
  public games: ListGamesModel[];

  constructor(private authService: AuthService) { }
  
  changeSource(event, videoId: string): void {
    event.target.src = `https://i.ytimg.com/vi/${videoId}/maxresdefault.jpg`;
  }
}