import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

import { tap } from 'rxjs/operators';

import { CartItemModel } from '../../../models/cart-item.model';
import { CartService } from '../../../services/cart.service';
import { AuthService } from 'src/app/sharedServices/auth.service';
import { OrderService } from '../../../services/order.service';

@Component({
  selector: 'app-list-items',
  templateUrl: './list-items.component.html',
  styleUrls: ['./list-items.component.css']
})
export class ListItemsComponent implements OnInit {
  public totalPrice: number;
  public games: CartItemModel[] = [];

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
    this.games = this.games.filter(g => g.id !== id);
    this.totalPrice = this.calculateTotalPrice();
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

  public clear(): void {
    this.cartService.clear();
    this.games = [];
  }

  private getGames(): void {
    this.cartService
      .getCart$()
      .pipe(
        tap((res: CartItemModel[]) => {
          this.games = res;
          this.totalPrice = this.calculateTotalPrice();
        })
      )
      .subscribe();
  }

  private calculateTotalPrice(): number {
    if (this.games.length === 0) {
      return 0;
    }

    return this.games
      .map((i: CartItemModel) => i.price)
      .reduce((x: number, y: number) => x + y);
  }
}
