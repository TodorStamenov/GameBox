import { ICategoriesState } from './categories.state';
import { CategoryActionTypes, Types } from './categories.actions';
import { ICategoryMenuModel } from 'src/app/modules/category/models/category-menu.model';
import { ICategoriesListModel } from 'src/app/modules/category/models/categories-list.model';
import { ICategoryBindingModel } from 'src/app/modules/category/models/category-binding.model';

const initialState: ICategoriesState = {
  all: [],
  toEdit: null,
  names: []
};

function getAllCategories(state: ICategoriesState, allCategories: ICategoriesListModel[]): ICategoriesState {
  return {
    ...state,
    all: allCategories
  };
}

function getCategoryToEdit(state: ICategoriesState, categoryToEdit: ICategoryBindingModel): ICategoriesState {
  return {
    ...state,
    toEdit: categoryToEdit
  };
}

function getCategoryNames(state: ICategoriesState, categoryNames: ICategoryMenuModel[]): ICategoriesState {
  return {
    ...state,
    names: categoryNames
  };
}

export function categoriesReducer(state = initialState, action: Types): ICategoriesState {
  switch (action.type) {
    case CategoryActionTypes.GetAllCategories:
      return getAllCategories(state, action.payload);

    case CategoryActionTypes.GetCategoryToEdit:
      return getCategoryToEdit(state, action.payload);

    case CategoryActionTypes.GetCategoryNames:
      return getCategoryNames(state, action.payload);

    default:
      return state;
  }
}
