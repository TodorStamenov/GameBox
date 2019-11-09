import { Component, OnInit } from '@angular/core';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { IGameListItemModel } from '~/app/modules/core/models/game-list-item.model';
import { CartService } from '../../services/cart.service';
import { RouterExtensions } from 'nativescript-angular/router';

@Component({
  selector: 'ns-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  public loading = false;
  public games$: Observable<IGameListItemModel[]>;

  constructor(
    private cartService: CartService,
    private router: RouterExtensions
  ) { }

  public ngOnInit(): void {
    this.games$ = this.cartService.getCart$().pipe(
      map(games => games = games.map(game => this.changeThumbnailUrls(game)))
    );
  }

  public changeSource(event: any, videoId: string): void {
    event.target.src = `https://i.ytimg.com/vi/${videoId}/maxresdefault.jpg`;
  }

  public onCreateOrder(): void {
    this.cartService.createOrder$()
      .subscribe(() => {
        this.cartService.clear();
        this.router.navigate(['/'], {
          transition: { name: 'slideLeft' }
        });
      });
  }

  public onRemoveItem(id: string): void {
    this.cartService.removeItem(id);
  }

  public onClear(): void {
    this.cartService.clear();
  }

  public calculateTotalPrice(games: IGameListItemModel[]): number {
    if (games.length === 0) {
      return 0;
    }

    return games.map(i => i.price).reduce((x, y) => x + y);
  }

  private changeThumbnailUrls(game: IGameListItemModel): IGameListItemModel {
    if (!game.thumbnailUrl) {
      game.thumbnailUrl = `https://i.ytimg.com/vi/${game.videoId}/maxresdefault.jpg`;
    }

    return game;
  }
}
