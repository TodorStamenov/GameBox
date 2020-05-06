import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { constants } from '~/app/common';
import { IGameListItemModel } from '../modules/core/models/game-list-item.model';
import * as cacheService from 'tns-core-modules/application-settings';
import { UIService } from './ui.service';

const cartUrl = constants.host + 'cart/';
const ordersUrl = constants.host + 'orders';

@Injectable({
  providedIn: 'root'
})
export class CartService {
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
    return this.http.post<IGameListItemModel[]>(cartUrl, this.cart).pipe(
      map(games => games.map(game => {
        game.thumbnailUrl = this.uiService.changeThumbnailUrls(game.thumbnailUrl, game.videoId);
        return game;
      }))
    );
  }

  public createOrder$(): Observable<void> {
    return this.http.post<void>(ordersUrl, this.cart);
  }
}
