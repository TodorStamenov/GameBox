import { Component, OnInit } from '@angular/core';

import { Observable, BehaviorSubject } from 'rxjs';
import { switchMap } from 'rxjs/operators';

import { IGameListItemModel } from '~/app/modules/core/models/game-list-item.model';
import { CartService } from '../../../../services/cart.service';
import { UIService } from '~/app/services/ui.service';

@Component({
  selector: 'ns-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  private reloadGames$$ = new BehaviorSubject<void>(null);

  public games$: Observable<IGameListItemModel[]>;

  constructor(
    private cartService: CartService,
    private uiService: UIService
  ) { }

  public ngOnInit(): void {
    this.games$ = this.reloadGames$$.asObservable().pipe(
      switchMap(() => this.cartService.getCart$())
    );
  }

  public onCreateOrder(): void {
    this.cartService.createOrder$().subscribe(() => {
      this.cartService.clear();
      this.uiService.navigate('/');
    });
  }

  public onRemoveItem(id: string): void {
    this.cartService.removeItem(id);
    this.reloadGames$$.next();
  }

  public onClear(): void {
    this.cartService.clear();
    this.reloadGames$$.next();
  }

  public calculateTotalPrice(games: IGameListItemModel[]): number {
    if (games.length === 0) {
      return 0;
    }

    return games.map(i => i.price).reduce((x, y) => x + y);
  }
}
