import { Action } from '@ngrx/store';

import { ICategoriesListModel } from 'src/app/modules/category/models/categories-list.model';
import { ICategoryBindingModel } from 'src/app/modules/category/models/category-binding.model';
import { ICategoryMenuModel } from 'src/app/modules/category/models/category-menu.model';

export enum CategoryActionTypes {
  GetAllCategories = '[CATEGORIES] Get All',
  GetCategoryToEdit = '[CATEGORIES] Get Category To Edit',
  GetCategoryNames = '[CATEGORIES] Get Category Names'
}

export class GetAllCategories implements Action {
  readonly type = CategoryActionTypes.GetAllCategories;
  constructor(public payload: ICategoriesListModel[]) { }
}

export class GetCategoryToEdit implements Action {
  readonly type = CategoryActionTypes.GetCategoryToEdit;
  constructor(public payload: ICategoryBindingModel) { }
}

export class GetCategoryNames implements Action {
  readonly type = CategoryActionTypes.GetCategoryNames;
  constructor(public payload: ICategoryMenuModel[]) { }
}

export type Types = GetAllCategories
  | GetCategoryToEdit
  | GetCategoryNames;
