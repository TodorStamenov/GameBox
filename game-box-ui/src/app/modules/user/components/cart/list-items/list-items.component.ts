import { Component, OnInit } from '@angular/core';
import { CartItemModel } from '../../../models/cart-item.model';
import { CartService } from '../../../services/cart.service';
import { AuthService } from 'src/app/modules/authentication/auth.service';
import { Router } from '@angular/router';
import { OrderService } from '../../../services/order.service';

@Component({
  selector: 'app-list-items',
  templateUrl: './list-items.component.html'
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

  ngOnInit(): void {
    this.getGames();
  }

  changeSource(event, videoId: string): void {
    event.target.src = `https://i.ytimg.com/vi/${videoId}/maxresdefault.jpg`;
  }

  removeItem(id: string): void {
    this.cartService.removeItem(id);
    this.getGames();
  }

  order(): void {
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
    this.cartService
      .getCart()
      .subscribe(res => {
        this.totalPrice = res.length === 0 ? 0 : res.map(i => i.price).reduce((x, y) => x + y);
        this.games = res;
      });
  }
}