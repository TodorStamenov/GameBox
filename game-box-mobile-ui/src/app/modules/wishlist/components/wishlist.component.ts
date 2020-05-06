import { Component, OnInit } from '@angular/core';

import { Observable, BehaviorSubject } from 'rxjs';
import { switchMap } from 'rxjs/operators';

import { IGameListItemModel } from '../../core/models/game-list-item.model';
import { WishlistService } from '../../../services/wishlist.service';
import { CartService } from '../../../services/cart.service';
import { UIService } from '../../../services/ui.service';

@Component({
  selector: 'ns-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.scss'],
  moduleId: module.id
})
export class WishlistComponent implements OnInit {
  private reloadGames$$ = new BehaviorSubject<void>(null);

  public games$: Observable<IGameListItemModel[]>;

  constructor(
    private wishlistService: WishlistService,
    private cartService: CartService,
    private uiService: UIService
  ) { }

  public ngOnInit(): void {
    this.games$ = this.reloadGames$$.asObservable().pipe(
      switchMap(() => this.wishlistService.getItems$())
    );
  }

  public onAddItem(id: string): void {
    this.wishlistService.removeItem$(id)
      .subscribe(() => {
        this.reloadGames$$.next();
        this.cartService.addItem(id);
        this.uiService.navigate('/cart/items');
      });
  }

  public onRemoveItem(id: string): void {
    this.wishlistService.removeItem$(id)
      .subscribe(() => this.reloadGames$$.next());
  }

  public onClear(): void {
    this.wishlistService.clearItems$()
      .subscribe(() => this.reloadGames$$.next());
  }
}
