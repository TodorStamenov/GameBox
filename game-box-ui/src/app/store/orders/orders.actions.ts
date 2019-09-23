import { Action } from '@ngrx/store';

import { IOrdersListModel } from 'src/app/modules/order/models/orders-list.model';

export const GET_ALL_ORDERS = '[ORDERS] Get All';

export class GetAllOrders implements Action {
  type = GET_ALL_ORDERS;
  constructor(public payload: IOrdersListModel[]) { }
}

export type Types = GetAllOrders;
