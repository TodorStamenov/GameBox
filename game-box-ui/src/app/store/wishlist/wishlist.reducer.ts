import { IWishlistState } from './wishlist.state';
import { WishlistActionTypes, Types } from './wishlist.actions';
import { IGameListItemModel } from 'src/app/modules/core/models/game-list-item.model';

const initialState: IWishlistState = {
  all: []
};

function getWishlistItems(state: IWishlistState, allItems: IGameListItemModel[]): IWishlistState {
  return {
    ...state,
    all: allItems
  };
}

function removeWishlistItem(state: IWishlistState, itemId: string): IWishlistState {
  return {
    ...state,
    all: [...state.all].filter(i => i.id !== itemId)
  };
}

function clearWishlistItems(state: IWishlistState): IWishlistState {
  return {
    ...state,
    all: []
  };
}

export function wishlistReducer(state = initialState, action: Types): IWishlistState {
  switch (action.type) {
    case WishlistActionTypes.GetAllItems:
      return getWishlistItems(state, action.payload);

    case WishlistActionTypes.ClearItems:
      return clearWishlistItems(state);

    case WishlistActionTypes.RemoveItem:
      return removeWishlistItem(state, action.payload);

    default:
      return state;
  }
}
