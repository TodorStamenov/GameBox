import { Component, OnInit } from '@angular/core';

import { Observable, BehaviorSubject } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

import { IGameListItemModel } from '~/app/modules/core/models/game-list-item.model';
import { CartService } from '../../services/cart.service';
import { RouterExtensions } from 'nativescript-angular/router';

@Component({
  selector: 'ns-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  private reloadGames$$ = new BehaviorSubject<void>(null);

  public games$: Observable<IGameListItemModel[]>;

  constructor(
    private cartService: CartService,
    private router: RouterExtensions
  ) { }

  public ngOnInit(): void {
    this.games$ = this.reloadGames$$.asObservable().pipe(
      switchMap(() => this.cartService.getCart$().pipe(
        map(games => games.map(game => this.changeThumbnailUrls(game))),
      ))
    );
  }

  public onCreateOrder(): void {
    this.cartService.createOrder$().subscribe(() => {
      this.cartService.clear();
      this.router.navigate(['/'], {
        transition: { name: 'slideLeft' }
      });
    });
  }

  public onRemoveItem(id: string): void {
    this.cartService.removeItem(id);
    this.reloadGames$$.next();
  }

  public onClear(): void {
    this.cartService.clear();
    this.reloadGames$$.next();
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
