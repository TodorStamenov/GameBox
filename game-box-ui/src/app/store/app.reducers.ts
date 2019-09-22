import { categoriesReducer } from './categories/categories.reducer';
import { gamesReducer } from './games/games.reducer';

export const appReducers = {
  categories: categoriesReducer,
  games: gamesReducer
};
