import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { CartItemModel } from '../../../models/cart-item.model';
import { CartService } from '../../../services/cart.service';
import { AuthService } from 'src/app/sharedServices/auth.service';
import { OrderService } from '../../../services/order.service';

@Component({
  selector: 'app-list-items',
  templateUrl: './list-items.component.html'
})
export class ListItemsComponent implements OnInit {
  public totalPrice: number;
  public games$: Observable<CartItemModel[]>;

  constructor(
    private authService: AuthService,
    private cartService: CartService,
    private orderService: OrderService,
    private router: Router
  ) { }

  public ngOnInit(): void {
    this.getGames();
  }

  public changeSource(event: any, videoId: string): void {
    event.target.src = `https://i.ytimg.com/vi/${videoId}/maxresdefault.jpg`;
  }

  public removeItem(id: string): void {
    this.cartService.removeItem(id);
    this.getGames();
  }

  public order(): void {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/auth/login']);
      return;
    }

    this.orderService
      .createOrder(this.cartService.cart)
      .subscribe(() => {
        this.cartService.clear();
        this.router.navigate(['/']);
      });
  }

  private getGames(): void {
    this.games$ = this.cartService
      .getCart()
      .pipe(tap((res: any) => {
        this.totalPrice = res.length === 0
          ? 0
          : res.map((i: any) => i.price).reduce((x: number, y: number) => x + y);
      }));
  }
}
