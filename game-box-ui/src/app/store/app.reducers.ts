import { categoriesReducer } from './categories/categories.reducer';
import { gamesReducer } from './games/games.reducer';
import { ordersReducer } from './orders/orders.reducer';
import { usersReducer } from './users/users.reducer';
import { cartReducer } from './cart/cart.reducer';
import { wishlistReducer } from './wishlist/wishlist.reducer';

export const appReducers = {
  categories: categoriesReducer,
  cart: cartReducer,
  wishlist: wishlistReducer,
  games: gamesReducer,
  orders: ordersReducer,
  users: usersReducer
};
