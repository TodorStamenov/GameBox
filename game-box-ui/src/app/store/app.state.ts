import { ICategoriesState } from './categories/categories.state';
import { IGamesState } from './games/games.state';
import { IOrdersState } from './orders/orders.state';
import { IUsersState } from './users/users.state';

export interface IAppState {
  categories: ICategoriesState;
  games: IGamesState;
  orders: IOrdersState;
  users: IUsersState;
}
