import { Component, Input, ChangeDetectionStrategy } from '@angular/core';
import { trigger, transition, style, animate } from '@angular/animations';

import { IGamesHomeListModel } from '../../../models/games-home-list.model';
import { CartService } from '../../../../cart/services/cart.service';

@Component({
  selector: 'app-game-card-list',
  templateUrl: './game-card-list.component.html',
  styleUrls: ['./game-card-list.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  animations: [
    trigger('itemCard', [
      transition('void => *', [
        style({
          opacity: 0,
          transform: 'translateY(300px)'
        }),
        animate(500)
      ])
    ])
  ]
})
export class GameCardListComponent {
  @Input()
  public games: IGamesHomeListModel[];

  constructor(private cartService: CartService) { }

  public changeSource(event: any, videoId: string): void {
    event.target.src = `https://i.ytimg.com/vi/${videoId}/maxresdefault.jpg`;
  }

  public addItem(id: string): void {
    this.cartService.addItem(id);
  }
}
