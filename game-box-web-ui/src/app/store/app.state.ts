import { ICategoriesState } from './categories/categories.state';
import { IGamesState } from './games/games.state';
import { IOrdersState } from './orders/orders.state';
import { IUsersState } from './users/users.state';
import { ICartState } from './cart/cart.state';
import { IWishlistState } from './wishlist/wishlist.state';

export interface IAppState {
  categories: ICategoriesState;
  cart: ICartState;
  wishlist: IWishlistState;
  games: IGamesState;
  orders: IOrdersState;
  users: IUsersState;
}
