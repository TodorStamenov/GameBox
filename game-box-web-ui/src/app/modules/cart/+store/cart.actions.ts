import { Action } from '@ngrx/store';
import { IGameListItemModel } from 'src/app/modules/core/models/game-list-item.model';

export enum CartActionTypes {
  LoadAllItems = '[CART] Load All Items',
  GetAllItems = '[CART] Get All Items',
  UnloadItem = '[CART] Unload Item',
  RemoveItem = '[CART] Remove Item',
  UnloadItems = '[CART] Unload Items',
  ClearItems = '[CART] Clear Items'
}

export class LoadAllItems implements Action {
  readonly type = CartActionTypes.LoadAllItems;
}

export class GetAllItems implements Action {
  readonly type = CartActionTypes.GetAllItems;
  constructor(public payload: IGameListItemModel[]) { }
}

export class UnloadItem implements Action {
  readonly type = CartActionTypes.UnloadItem;
  constructor(public payload: string) { }
}

export class RemoveItem implements Action {
  readonly type = CartActionTypes.RemoveItem;
  constructor(public payload: string) { }
}

export class UnloadItems implements Action {
  readonly type = CartActionTypes.UnloadItems;
}

export class ClearItems implements Action {
  readonly type = CartActionTypes.ClearItems;
}

export type Types = LoadAllItems
  | GetAllItems
  | UnloadItem
  | ClearItems
  | UnloadItems
  | RemoveItem;
