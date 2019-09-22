import { ICategoriesListModel } from 'src/app/modules/admin/models/categories/categories-list.model';
import { ICategoryBindingModel } from 'src/app/modules/admin/models/categories/category-binding.model';
import { ICategoryMenuModel } from 'src/app/modules/admin/models/categories/category-menu.model';

export interface CategoriesState {
  all: ICategoriesListModel[];
  toEdit: ICategoryBindingModel;
  names: ICategoryMenuModel[];
}
