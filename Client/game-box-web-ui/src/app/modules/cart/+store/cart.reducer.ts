import { ICartState } from './cart.state';
import { CartActionTypes, Types } from './cart.actions';
import { IGameListItemModel } from 'src/app/modules/core/models/game-list-item.model';

const initialState: ICartState = {
  all: []
};

function getCartItems(state: ICartState, allItems: IGameListItemModel[]): ICartState {
  return {
    ...state,
    all: allItems
  };
}

function removeCartItem(state: ICartState, itemId: string): ICartState {
  return {
    ...state,
    all: [...state.all].filter(i => i.id !== itemId)
  };
}

function clearCartItems(state: ICartState): ICartState {
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

    case CartActionTypes.RemoveItem:
      return removeCartItem(state, action.payload);

    default:
      return state;
  }
}
