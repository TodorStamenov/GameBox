import { IOrdersListModel } from 'src/app/modules/order/models/orders-list.model';
import { IAppState } from 'src/app/store/app.state';

export interface IState extends IAppState {
  orders: IOrdersState;
}

export interface IOrdersState {
  all: IOrdersListModel[];
}
