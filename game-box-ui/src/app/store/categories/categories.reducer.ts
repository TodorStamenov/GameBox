import { ICategoriesState } from './categories.state';
import * as CategoriesActions from '../categories/categories.actions';

const initialState: ICategoriesState = {
  all: [],
  toEdit: null,
  names: []
};

function getAllCategories(state: ICategoriesState, allCategories: any) {
  return {
    ...state,
    all: allCategories
  };
}

function getCategoryToEdit(state: ICategoriesState, categoryToEdit: any) {
  return {
    ...state,
    toEdit: categoryToEdit
  };
}

function getCategoryNames(state: ICategoriesState, categoryNames: any) {
  return {
    ...state,
    names: categoryNames
  };
}

export function categoriesReducer(state: ICategoriesState = initialState, action: CategoriesActions.Types) {
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