import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { constants } from '../../../common';
import { ListCategoriesModel } from '../models/categories/list-categories.model';
import { CategoryBindingModel } from '../models/categories/category-binding.model';
import { CategoryMenuModel } from '../models/categories/category-menu.model';

const categoriesUrl = constants.host + 'categories/';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  constructor(private http: HttpClient) { }

  public getCategories(): Observable<ListCategoriesModel[]> {
    return this.http.get<ListCategoriesModel[]>(categoriesUrl);
  }

  public getCategory(id: string): Observable<ListCategoriesModel> {
    return this.http.get<ListCategoriesModel>(categoriesUrl + id);
  }

  public createCategory(body: CategoryBindingModel): Observable<CategoryBindingModel> {
    return this.http.post<CategoryBindingModel>(categoriesUrl, body);
  }

  public editCategory(id: string, body: CategoryBindingModel): Observable<CategoryBindingModel> {
    return this.http.put<CategoryBindingModel>(categoriesUrl + id, body);
  }

  public getCategoryNames(): Observable<CategoryMenuModel[]> {
    return this.http.get<CategoryMenuModel[]>(categoriesUrl + 'menu');
  }
}
