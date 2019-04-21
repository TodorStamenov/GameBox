import { Component, Input } from '@angular/core';

import { ListGamesModel } from '../../../models/list-games.model';
import { CartService } from '../../../services/cart.service';

@Component({
  selector: 'app-game-card',
  templateUrl: './game-card.component.html',
  styleUrls: ['./game-card.component.css']
})
export class GameCardComponent {
  @Input()
  public games: ListGamesModel[];

  constructor(private cartService: CartService) { }

  public changeSource(event: any, videoId: string): void {
    event.target.src = `https://i.ytimg.com/vi/${videoId}/maxresdefault.jpg`;
  }

  public addItem(id: string): void {
    this.cartService.addItem(id);
  }
}
