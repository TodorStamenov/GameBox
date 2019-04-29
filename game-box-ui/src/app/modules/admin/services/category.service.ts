import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { constants } from '../../../common';
import { ICategoriesListModel } from '../models/categories/categories-list.model';
import { ICategoryBindingModel } from '../models/categories/category-binding.model';
import { ICategoryMenuModel } from '../models/categories/category-menu.model';

const categoriesUrl = constants.host + 'categories/';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  constructor(private http: HttpClient) { }

  public getCategories$(): Observable<ICategoriesListModel[]> {
    return this.http.get<ICategoriesListModel[]>(categoriesUrl);
  }

  public getCategory$(id: string): Observable<ICategoriesListModel> {
    return this.http.get<ICategoriesListModel>(categoriesUrl + id);
  }

  public createCategory$(body: ICategoryBindingModel): Observable<ICategoryBindingModel> {
    return this.http.post<ICategoryBindingModel>(categoriesUrl, body);
  }

  public editCategory$(id: string, body: ICategoryBindingModel): Observable<ICategoryBindingModel> {
    return this.http.put<ICategoryBindingModel>(categoriesUrl + id, body);
  }

  public getCategoryNames$(): Observable<ICategoryMenuModel[]> {
    return this.http.get<ICategoryMenuModel[]>(categoriesUrl + 'menu');
  }
}
