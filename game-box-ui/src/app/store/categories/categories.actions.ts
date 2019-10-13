import { Action } from '@ngrx/store';

import { ICategoriesListModel } from 'src/app/modules/category/models/categories-list.model';
import { ICategoryBindingModel } from 'src/app/modules/category/models/category-binding.model';
import { ICategoryMenuModel } from 'src/app/modules/category/models/category-menu.model';

export enum CategoryActionTypes {
  LoadAllCategories = '[CATEGORIES] Load All Categories',
  GetAllCategories = '[CATEGORIES] Get All Categories',
  LoadCategoryToEdit = '[CATEGORIES] Load Category To Edit',
  GetCategoryToEdit = '[CATEGORIES] Get Category To Edit',
  LoadCategoryNames = '[CATEGORIES] Load Category Names',
  GetCategoryNames = '[CATEGORIES] Get Category Names'
}

export class LoadAllCategories {
  readonly type = CategoryActionTypes.LoadAllCategories;
}

export class GetAllCategories implements Action {
  readonly type = CategoryActionTypes.GetAllCategories;
  constructor(public payload: ICategoriesListModel[]) { }
}

export class LoadCategoryToEdit implements Action {
  readonly type = CategoryActionTypes.LoadCategoryToEdit;
  constructor(public payload: string) { }
}

export class GetCategoryToEdit implements Action {
  readonly type = CategoryActionTypes.GetCategoryToEdit;
  constructor(public payload: ICategoryBindingModel) { }
}

export class LoadCategoryNames implements Action {
  readonly type = CategoryActionTypes.LoadCategoryNames;
}

export class GetCategoryNames implements Action {
  readonly type = CategoryActionTypes.GetCategoryNames;
  constructor(public payload: ICategoryMenuModel[]) { }
}

export type Types = LoadAllCategories
  | GetAllCategories
  | LoadCategoryToEdit
  | GetCategoryToEdit
  | LoadCategoryNames
  | GetCategoryNames;
