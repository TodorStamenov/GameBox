import { Action } from '@ngrx/store';

import { IOrdersListModel } from 'src/app/modules/order/models/orders-list.model';

export enum OrderActionTypes {
  LoadAllOrders = '[ORDERS] Load All Orders',
  GetAllOrders = '[ORDERS] Get All Orders'
}

export class LoadAllOrders implements Action {
  readonly type = OrderActionTypes.LoadAllOrders;
  constructor(public payload: { startDate: string, endDate: string }) { }
}

export class GetAllOrders implements Action {
  readonly type = OrderActionTypes.GetAllOrders;
  constructor(public payload: IOrdersListModel[]) { }
}

export type Types = LoadAllOrders
  | GetAllOrders;
