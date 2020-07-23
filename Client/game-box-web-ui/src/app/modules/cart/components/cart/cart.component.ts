import { Router } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';

import { IState } from '../../+store/cart.state';
import { IGameListItemModel } from '../../../core/models/game-list-item.model';
import { CartService } from '../../../../services/cart.service';
import { AuthService } from 'src/app/services/auth.service';
import { OrderService } from 'src/app/services/order.service';
import { LoadAllItems, ClearItems, UnloadItems, UnloadItem  } from 'src/app/modules/cart/+store/cart.actions';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html'
})
export class CartComponent implements OnInit, OnDestroy {
  public games$: Observable<IGameListItemModel[]>;

  constructor(
    private authService: AuthService,
    private cartService: CartService,
    private orderService: OrderService,
    private router: Router,
    private store: Store<IState>
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

  public onCreateOrder(): void {
    if (!this.authService.isAuthed) {
      this.router.navigate(['/auth/login']);
      return;
    }

    this.orderService.createOrder$(this.cartService.cart)
      .subscribe(() => {
        this.store.dispatch(new UnloadItems());
        this.router.navigate(['/games/owned']);
      });
  }

  public onRemoveItem(id: string): void {
    this.store.dispatch(new UnloadItem(id));
  }

  public onClear(): void {
    this.store.dispatch(new UnloadItems());
  }

  public calculateTotalPrice(games: IGameListItemModel[]): number {
    if (games.length === 0) {
      return 0;
    }

    return games.map(i => i.price).reduce((x, y) => x + y);
  }
}
