import { Action } from '@ngrx/store';
import { IGameListItemModel } from 'src/app/modules/core/models/game-list-item.model';

export enum WishlistActionTypes {
  LoadAllItems = '[WISHLIST] Load All Items',
  GetAllItems = '[WISHLIST] Get All Items',
  RemoveItem = '[WISHLIST] Remove Item',
  ClearItems = '[WISHLIST] Clear Items'
}

export class LoadAllItems implements Action {
  readonly type = WishlistActionTypes.LoadAllItems;
}

export class GetAllItems implements Action {
  readonly type = WishlistActionTypes.GetAllItems;
  constructor(public payload: IGameListItemModel[]) { }
}

export class RemoveItem implements Action {
  readonly type = WishlistActionTypes.RemoveItem;
  constructor(public payload: string) { }
}

export class ClearItems implements Action {
  readonly type = WishlistActionTypes.ClearItems;
}

export type Types = LoadAllItems
  | GetAllItems
  | ClearItems
  | RemoveItem;
