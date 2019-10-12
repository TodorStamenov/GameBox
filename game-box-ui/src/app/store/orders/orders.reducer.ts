import { IOrdersState } from './orders.state';
import { OrderActionTypes, Types } from './orders.actions';
import { IOrdersListModel } from 'src/app/modules/order/models/orders-list.model';

const initialState: IOrdersState = {
  all: []
};

function getAllOrders(state: IOrdersState, allActions: IOrdersListModel[]): IOrdersState {
  return {
    ...state,
    all: allActions
  };
}

export function ordersReducer(state = initialState, action: Types): IOrdersState {
  switch (action.type) {
    case OrderActionTypes.GetAllOrders:
      return getAllOrders(state, action.payload);

    default:
      return state;
  }
}
