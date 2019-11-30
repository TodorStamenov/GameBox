import { Component, Input, Output, EventEmitter } from '@angular/core';
import { trigger, transition, animate, style } from '@angular/animations';

import { IGameListItemModel } from '../../models/game-list-item.model';

@Component({
  selector: 'app-game-list-items',
  templateUrl: './game-list-items.component.html',
  styleUrls: ['./game-list-items.component.scss'],
  animations: [
    trigger('itemCard', [
      transition('* => void', [
        animate(500, style({
          transform: 'translateX(300px)',
          opacity: 0
        }))
      ])
    ])
  ]
})
export class GameListItemsComponent {
  @Input() public games: IGameListItemModel[] = [];
  @Input() public showBuyButton = false;

  @Output() public buyItemClick = new EventEmitter<string>();
  @Output() public removeItemClick = new EventEmitter<string>();

  public changeSource(event: any, videoId: string): void {
    event.target.src = `https://i.ytimg.com/vi/${videoId}/maxresdefault.jpg`;
  }

  public onRemoveItem(id: string): void {
    this.removeItemClick.emit(id);
  }

  public onBuyItem(id: string): void {
    this.buyItemClick.emit(id);
  }
}
