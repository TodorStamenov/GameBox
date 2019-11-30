import { IGameListItemModel } from 'src/app/modules/core/models/game-list-item.model';
import { IAppState } from 'src/app/store/app.state';

export interface IState extends IAppState {
  wishlist: IWishlistState;
}

export interface IWishlistState {
  all: IGameListItemModel[];
}
