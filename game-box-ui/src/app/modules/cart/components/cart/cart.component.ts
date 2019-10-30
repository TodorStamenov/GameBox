import { Router } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';

import { IAppState } from 'src/app/store/app.state';
import { IGameListItemModel } from '../../../core/models/game-list-item.model';
import { CartService } from '../../services/cart.service';
import { AuthHelperService } from 'src/app/modules/core/services/auth-helper.service';
import { OrderService } from 'src/app/modules/order/services/order.service';
import { LoadAllItems, ClearItems, RemoveItem  } from 'src/app/store/cart/cart.actions';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit, OnDestroy {
  public games$: Observable<IGameListItemModel[]>;

  constructor(
    private authHelperService: AuthHelperService,
    private cartService: CartService,
    private orderService: OrderService,
    private router: Router,
    private store: Store<IAppState>
  ) { }

  public ngOnInit(): void {
    this.store.dispatch(new LoadAllItems());
    this.games$ = this.store.pipe(
      select(s => s.cart.all)
    );
  }

  public ngOnDestroy(): void {
    this.store.dispatch(new ClearItems());
  }

  public changeSource(event: any, videoId: string): void {
    event.target.src = `https://i.ytimg.com/vi/${videoId}/maxresdefault.jpg`;
  }

  public onCreateOrder(): void {
    if (!this.authHelperService.isAuthenticated()) {
      this.router.navigate(['/auth/login']);
      return;
    }

    this.orderService
      .createOrder$(this.cartService.cart)
      .subscribe(() => {
        this.cartService.clear();
        this.store.dispatch(new ClearItems());
        this.router.navigate(['/games/owned']);
      });
  }

  public onRemoveItem(id: string): void {
    this.store.dispatch(new RemoveItem(id));
    this.cartService.removeItem(id);
  }

  public onClear(): void {
    this.cartService.clear();
    this.store.dispatch(new ClearItems());
  }

  public calculateTotalPrice(games: IGameListItemModel[]): number {
    if (games.length === 0) {
      return 0;
    }

    return games.map(i => i.price).reduce((x, y) => x + y);
  }
}
