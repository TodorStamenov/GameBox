import { ICategoriesState } from './categories/categories.state';
import { IGamesState } from './games/games.state';

export interface IAppState {
  categories: ICategoriesState;
  games: IGamesState;
}
