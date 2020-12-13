import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Router } from '@angular/router';

import { Observable } from 'rxjs';

import { IGameListItemModel } from 'src/app/modules/core/models/game-list-item.model';
import { LoadAllItems, ClearItems, UnloadItem, UnloadItems } from 'src/app/modules/wishlist/+store/wishlist.actions';
import { IState } from '../../+store/wishlist.state';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.scss']
})
export class WishlistComponent implements OnInit, OnDestroy {
  public totalPrice: number;
  public games$: Observable<IGameListItemModel[]>;

  constructor(
    private store: Store<IState>,
    private cartService: CartService,
    private router: Router,
  ) { }

  public ngOnInit(): void {
    this.store.dispatch(new LoadAllItems());
    this.games$ = this.store.pipe(
      select(s => s.wishlist.all)
    );
  }

  public ngOnDestroy(): void {
    this.store.dispatch(new ClearItems());
  }

  public onClearItems(): void {
    this.store.dispatch(new UnloadItems());
  }

  public onRemoveItem(id: string): void {
    this.store.dispatch(new UnloadItem(id));
  }

  public onBuyItem(id: string): void {
    this.store.dispatch(new UnloadItem(id));
    this.cartService
      .addItem$(id)
      .subscribe(() => this.router.navigate(['/cart/items']));
  }
}
