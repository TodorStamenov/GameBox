import { ICartState } from './cart.state';
import { CartActionTypes, Types } from './cart.actions';
import { ICartItemsListModel } from 'src/app/modules/cart/models/cart-items-list.model';

const initialState: ICartState = {
  all: []
};

function getCartItems(state: ICartState, allItems: ICartItemsListModel[]) {
  return {
    ...state,
    all: allItems
  };
}

function clearCartItems(state: ICartState) {
  return {
    ...state,
    all: []
  };
}

export function cartReducer(state = initialState, action: Types): ICartState {
  switch (action.type) {
    case CartActionTypes.GetAllItems:
      return getCartItems(state, action.payload);

    case CartActionTypes.ClearItems:
      return clearCartItems(state);

    default:
      return state;
  }
}
