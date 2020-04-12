import { ICoreState } from './+store/core/core.state';
import { ICategoriesState } from './+store/category/categories.state';

export interface IAppState {
  core: ICoreState;
  categories: ICategoriesState;
}
