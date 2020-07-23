import { IGameListItemModel } from 'src/app/modules/core/models/game-list-item.model';
import { IAppState } from '../../../store/app.state';

export interface IState extends IAppState {
  cart: ICartState;
}

export interface ICartState {
  all: IGameListItemModel[];
}
