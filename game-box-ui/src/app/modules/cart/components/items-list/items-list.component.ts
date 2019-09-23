import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { trigger, transition, animate, style } from '@angular/animations';

import { tap } from 'rxjs/operators';

import { ICartItemsListModel } from '../../models/cart-items-list.model';
import { CartService } from '../../services/cart.service';
import { AuthHelperService } from 'src/app/modules/core/services/auth-helper.service';
import { OrderService } from 'src/app/modules/order/services/order.service';

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
export class ItemsListComponent implements OnInit {
  public totalPrice: number;
  public games: ICartItemsListModel[] = [];

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

  private getGames(): void {
    this.cartService
      .getCart$()
      .pipe(
        tap((res: ICartItemsListModel[]) => {
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
      .map((i: ICartItemsListModel) => i.price)
      .reduce((x: number, y: number) => x + y);
  }
}
