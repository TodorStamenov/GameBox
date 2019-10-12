import { Action } from '@ngrx/store';

import { IOrdersListModel } from 'src/app/modules/order/models/orders-list.model';

export enum OrderActionTypes {
  GetAllOrders = '[ORDERS] Get All'
}

export class GetAllOrders implements Action {
  readonly type = OrderActionTypes.GetAllOrders;
  constructor(public payload: IOrdersListModel[]) { }
}

export type Types = GetAllOrders;
