import { categoriesReducer } from './categories/categories.reducer';
import { gamesReducer } from './games/games.reducer';
import { ordersReducer } from './orders/orders.reducer';

export const appReducers = {
  categories: categoriesReducer,
  games: gamesReducer,
  orders: ordersReducer
};
