import { CategoriesState } from './categories.state';
import * as CategoriesActions from '../categories/categories.actions';

const initialState: CategoriesState = {
  all: [],
  toEdit: null,
  names: []
};

function getAllCategories(state: CategoriesState, allCategories: any) {
  return {
    ...state,
    all: allCategories
  };
}

function getCategoryToEdit(state: CategoriesState, categoryToEdit: any) {
  return {
    ...state,
    toEdit: categoryToEdit
  };
}

function getCategoryNames(state: CategoriesState, categoryNames: any) {
  return {
    ...state,
    names: categoryNames
  };
}

export function categoriesReducer(state: CategoriesState = initialState, action: CategoriesActions.Types) {
  switch (action.type) {
    case CategoriesActions.GET_ALL_CATEGORIES:
      return getAllCategories(state, action.payload);
    case CategoriesActions.GET_CATEGORY_TO_EDIT:
      return getCategoryToEdit(state, action.payload);
    case CategoriesActions.GET_CATEGORY_NAMES:
      return getCategoryNames(state, action.payload);
    default:
      return state;
  }
}
