import { Action } from '@ngrx/store';
import { ICartItemsListModel } from 'src/app/modules/cart/models/cart-items-list.model';

export enum CartActionTypes {
  LoadAllItems = '[CART] Load All Items',
  GetAllItems = '[CART] Get All Items',
  ClearItems = '[CART] Clear Cart Items'
}

export class LoadAllItems implements Action {
  readonly type = CartActionTypes.LoadAllItems;
}

export class GetAllItems implements Action {
  readonly type = CartActionTypes.GetAllItems;
  constructor(public payload: ICartItemsListModel[]) { }
}

export class ClearCartItems implements Action {
  readonly type = CartActionTypes.ClearItems;
}

export type Types = LoadAllItems
  | GetAllItems
  | ClearCartItems;
