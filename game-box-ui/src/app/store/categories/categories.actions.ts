import { Action } from '@ngrx/store';

import { ICategoriesListModel } from 'src/app/modules/admin/models/categories/categories-list.model';
import { ICategoryBindingModel } from 'src/app/modules/admin/models/categories/category-binding.model';
import { ICategoryMenuModel } from 'src/app/modules/admin/models/categories/category-menu.model';

export const GET_ALL_CATEGORIES = '[CATEGORIES] Get All';
export const GET_CATEGORY_TO_EDIT = '[CATEGORIES] Get Category To Edit';
export const GET_CATEGORY_NAMES = '[CATEGORIES] Get Category Names';

export class GetAllCategories implements Action {
  type = GET_ALL_CATEGORIES;
  constructor(public payload: ICategoriesListModel[]) { }
}

export class GetCategoryToEdit implements Action {
  type = GET_CATEGORY_TO_EDIT;
  constructor(public payload: ICategoryBindingModel) { }
}

export class GetCategoryNames implements Action {
  type = GET_CATEGORY_NAMES;
  constructor(public payload: ICategoryMenuModel[]) { }
}

export type Types = GetAllCategories | GetCategoryToEdit | GetCategoryNames;
