import { ICategoriesState } from './categories/categories.state';
import { IGamesState } from './games/games.state';
import { IOrdersState } from './orders/orders.state';

export interface IAppState {
  categories: ICategoriesState;
  games: IGamesState;
  orders: IOrdersState;
}
