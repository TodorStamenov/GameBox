import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';

import { mergeMap, map } from 'rxjs/operators';

import { CategoryService } from 'src/app/services/category.service';
import {
  CategoryActionTypes,
  GetAllCategories,
  LoadCategoryToEdit,
  GetCategoryToEdit,
  GetCategoryNames
} from './categories.actions';

@Injectable()
export class CategoriesEffects {
  constructor(
    private actions$: Actions,
    private categoryService: CategoryService
  ) { }

  loadCategories$ = createEffect(() => this.actions$.pipe(
    ofType(CategoryActionTypes.LoadAllCategories),
    mergeMap(() => this.categoryService.getCategories$().pipe(
      map(categories => new GetAllCategories(categories))
    ))
  ));

  loadCategory$ = createEffect(() => this.actions$.pipe(
    ofType(CategoryActionTypes.LoadCategoryToEdit),
    mergeMap((action: LoadCategoryToEdit) => this.categoryService.getCategory$(action.payload).pipe(
      map((category => new GetCategoryToEdit(category)))
    ))
  ));

  loadCategoryNames$ = createEffect(() => this.actions$.pipe(
    ofType(CategoryActionTypes.LoadCategoryNames),
    mergeMap(() => this.categoryService.getCategoryNames$().pipe(
      map((categories => new GetCategoryNames(categories)))
    ))
  ));
}
