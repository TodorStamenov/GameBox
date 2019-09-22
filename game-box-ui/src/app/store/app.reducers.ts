import { categoriesReducer } from './categories/categories.reducer';
import { gamesReducer } from './games/games.reducer';
import { ordersReducer } from './orders/orders.reducer';
import { usersReducer } from './users/users.reducer';

export const appReducers = {
  categories: categoriesReducer,
  games: gamesReducer,
  orders: ordersReducer,
  users: usersReducer
};
