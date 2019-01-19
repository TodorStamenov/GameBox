import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { constants } from 'src/app/common';
import { CartItemModel } from '../models/cart-item.model';

const cartUrl = constants.host + 'cart/';

@Injectable()
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

  public addItem(id: string): void {
    const items = this.cart;
    items.push(id);
    this.cart = items;
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

  public getCart(): Observable<CartItemModel[]> {
    return this.http.post<CartItemModel[]>(cartUrl, this.cart);
  }
}
