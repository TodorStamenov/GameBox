import { HttpClient } from "@angular/common/http";
import { CartItemModel } from "../models/cart-item.model";
import { constants } from "src/app/common";
import { Injectable } from "@angular/core";

const cartUrl = constants.host + 'cart/'

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

  addItem(id: string): void {
    let items = this.cart;
    items.push(id);
    this.cart = items;
  }

  removeItem(id: string): void {
    let items = new Set(this.cart);

    if (!items || !items.has(id)) {
      return;
    }

    items.delete(id);
    this.cart = Array.from(items);
  }

  clear() {
    this.cart = [];
  }

  getCart() {
    return this.http.post<CartItemModel[]>(cartUrl, this.cart);
  }
}