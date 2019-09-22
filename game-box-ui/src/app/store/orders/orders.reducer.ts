import { IOrdersState } from './orders.state';
import * as OrdersActions from './orders.actions';

const initialState: IOrdersState = {
  all: []
};

function getAllOrders(state: IOrdersState, allActions: any) {
  return {
    ...state,
    all: allActions
  };
}

export function ordersReducer(state: IOrdersState = initialState, action: OrdersActions.Types) {
  switch (action.type) {
    case OrdersActions.GET_ALL_ORDERS:
      return getAllOrders(state, action.payload);
    default:
      return state;
  }
}
