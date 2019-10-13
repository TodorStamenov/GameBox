import { Router } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { trigger, transition, animate, style } from '@angular/animations';
import { Store, select } from '@ngrx/store';

import { takeWhile } from 'rxjs/operators';

import { IAppState } from 'src/app/store/app.state';
import { ICartItemsListModel } from '../../models/cart-items-list.model';
import { CartService } from '../../services/cart.service';
import { AuthHelperService } from 'src/app/modules/core/services/auth-helper.service';
import { OrderService } from 'src/app/modules/order/services/order.service';
import { LoadAllItems, ClearCartItems } from 'src/app/store/cart/cart.actions';

@Component({
  selector: 'app-items-list',
  templateUrl: './items-list.component.html',
  styleUrls: ['./items-list.component.scss'],
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
export class ItemsListComponent implements OnInit, OnDestroy {
  private componentActive = true;

  public totalPrice: number;
  public games: ICartItemsListModel[] = [];

  constructor(
    private authHelperService: AuthHelperService,
    private cartService: CartService,
    private orderService: OrderService,
    private router: Router,
    private store: Store<IAppState>
  ) { }

  public ngOnInit(): void {
    this.store.dispatch(new LoadAllItems());
    this.store.pipe(
      select(s => s.cart.all),
      takeWhile(() => this.componentActive)
    )
    .subscribe(items => {
      this.games = items;
      this.totalPrice = this.calculateTotalPrice();
    });
  }

  public ngOnDestroy(): void {
    this.componentActive = false;
    this.store.dispatch(new ClearCartItems());
  }

  public changeSource(event: any, videoId: string): void {
    event.target.src = `https://i.ytimg.com/vi/${videoId}/maxresdefault.jpg`;
  }

  public removeItem(id: string): void {
    this.cartService.removeItem(id);
    this.games = this.games.filter(g => g.id !== id);
    this.totalPrice = this.calculateTotalPrice();
  }

  public order(): void {
    if (!this.authHelperService.isAuthenticated()) {
      this.router.navigate(['/auth/login']);
      return;
    }

    this.orderService
      .createOrder$(this.cartService.cart)
      .subscribe(() => {
        this.cartService.clear();
        this.router.navigate(['/games/owned']);
      });
  }

  public clear(): void {
    this.cartService.clear();
    this.games = [];
  }

  private calculateTotalPrice(): number {
    if (this.games.length === 0) {
      return 0;
    }

    return this.games
      .map((i: ICartItemsListModel) => i.price)
      .reduce((x: number, y: number) => x + y);
  }
}
