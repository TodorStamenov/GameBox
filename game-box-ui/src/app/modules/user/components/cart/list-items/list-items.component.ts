import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { trigger, transition, animate, style } from '@angular/animations';

import { tap } from 'rxjs/operators';

import { ICartItemModel } from '../../../models/cart-item.model';
import { CartService } from '../../../services/cart.service';
import { OrderService } from '../../../services/order.service';
import { AuthHelperService } from 'src/app/modules/core/services/auth-helper.service';

@Component({
  selector: 'app-list-items',
  templateUrl: './list-items.component.html',
  styleUrls: ['./list-items.component.css'],
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
export class ListItemsComponent implements OnInit {
  public totalPrice: number;
  public games: ICartItemModel[] = [];

  constructor(
    private authHelperService: AuthHelperService,
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
    if (!this.authHelperService.isAuthenticated()) {
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
        tap((res: ICartItemModel[]) => {
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
      .map((i: ICartItemModel) => i.price)
      .reduce((x: number, y: number) => x + y);
  }
}
