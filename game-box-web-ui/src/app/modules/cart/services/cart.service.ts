import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, of } from 'rxjs';

import { constants } from 'src/app/common';
import { IGameListItemModel } from '../../core/models/game-list-item.model';

const cartUrl = constants.apiHost + 'cart/';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  constructor(private http: HttpClient) { }

  get cart(): string[] {
    if (!localStorage.getItem('cart')) {
      localStorage.setItem('cart', JSON.stringify([]));
    }

    return JSON.parse(localStorage.getItem('cart'));
  }

  set cart(items: string[]) {
    localStorage.setItem('cart', JSON.stringify(Array.from(new Set(items))));
  }

  public addItem$(id: string): Observable<string> {
    this.cart = [...this.cart, id];
    return of(id);
  }

  public removeItem$(id: string): Observable<string> {
    const items = new Set(this.cart);

    if (!items || !items.has(id)) {
      return;
    }

    items.delete(id);
    this.cart = Array.from(items);

    return of(id);
  }

  public clear$(): Observable<[]> {
    this.cart = [];
    return of([]);
  }

  public getCart$(): Observable<IGameListItemModel[]> {
    return this.http.post<IGameListItemModel[]>(cartUrl, this.cart);
  }
}
