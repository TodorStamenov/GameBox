import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Store } from '@ngrx/store';

import { AppState } from 'src/app/store/app.state';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { constants } from '../../../common';
import { ICategoriesListModel } from '../models/categories/categories-list.model';
import { ICategoryBindingModel } from '../models/categories/category-binding.model';
import { ICategoryMenuModel } from '../models/categories/category-menu.model';
import { GetAllCategories, GetCategoryToEdit, GetCategoryNames } from 'src/app/store/categories/categories.actions';

const categoriesUrl = constants.host + 'categories/';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  constructor(
    private http: HttpClient,
    private store: Store<AppState>
  ) { }

  public getCategories$(): Observable<void> {
    return this.http.get<ICategoriesListModel[]>(categoriesUrl).pipe(
      map((categories: ICategoriesListModel[]) => {
        this.store.dispatch(new GetAllCategories(categories));
      })
    );
  }

  public getCategory$(id: string): Observable<void> {
    return this.http.get<ICategoriesListModel>(categoriesUrl + id).pipe(
      map((category: ICategoryBindingModel) => {
        this.store.dispatch(new GetCategoryToEdit(category));
      })
    );
  }

  public createCategory$(body: ICategoryBindingModel): Observable<ICategoryBindingModel> {
    return this.http.post<ICategoryBindingModel>(categoriesUrl, body);
  }

  public editCategory$(id: string, body: ICategoryBindingModel): Observable<ICategoryBindingModel> {
    return this.http.put<ICategoryBindingModel>(categoriesUrl + id, body);
  }

  public getCategoryNames$(): Observable<void> {
    return this.http.get<ICategoryMenuModel[]>(categoriesUrl + 'menu').pipe(
      map((categories: ICategoryMenuModel[]) => {
        this.store.dispatch(new GetCategoryNames(categories));
      })
    );
  }
}
