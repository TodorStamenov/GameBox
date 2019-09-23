import { ICategoriesListModel } from 'src/app/modules/category/models/categories-list.model';
import { ICategoryBindingModel } from 'src/app/modules/category/models/category-binding.model';
import { ICategoryMenuModel } from 'src/app/modules/category/models/category-menu.model';

export interface ICategoriesState {
  all: ICategoriesListModel[];
  toEdit: ICategoryBindingModel;
  names: ICategoryMenuModel[];
}
