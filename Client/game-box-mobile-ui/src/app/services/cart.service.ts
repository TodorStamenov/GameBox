import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as cacheService from '@nativescript/core/application-settings';

import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';

import { IGameListItemModel } from '../modules/core/models/game-list-item.model';
import { UIService } from './ui.service';
import { constants } from '~/app/common';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartUrl = constants.gameBoxApiUrl + 'cart/';
  private ordersUrl = constants.gameBoxApiUrl + 'orders';

  constructor(
    private http: HttpClient,
    private uiService: UIService
  ) { }

  get cart(): string[] {
    if (!cacheService.getString('cart')) {
      cacheService.setString('cart', JSON.stringify([]));
    }

    return JSON.parse(cacheService.getString('cart'));
  }

  set cart(items: string[]) {
    cacheService.setString(
      'cart',
      JSON.stringify(Array.from(new Set(items)))
    );
  }

  public addItem(id: string): void {
    this.cart = [...this.cart, id];
  }

  public removeItem(id: string): void {
    const items = new Set(this.cart);

    if (!items || !items.has(id)) {
      return;
    }

    items.delete(id);
    this.cart = Array.from(items);
  }

  public clear(): void {
    this.cart = [];
  }

  public getCart$(): Observable<IGameListItemModel[]> {
    return this.http.post<IGameListItemModel[]>(this.cartUrl, this.cart).pipe(
      take(1),
      map(games => games.map(game => {
        game.thumbnailUrl = this.uiService.changeThumbnailUrls(game.thumbnailUrl, game.videoId);
        return game;
      }))
    );
  }

  public createOrder$(): Observable<void> {
    return this.http.post<void>(this.ordersUrl, this.cart).pipe(take(1));
  }
}
