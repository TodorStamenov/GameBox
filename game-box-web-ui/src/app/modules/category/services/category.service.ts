import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { ICategoriesListModel } from '../models/categories-list.model';
import { ICategoryBindingModel } from '../models/category-binding.model';
import { ICategoryMenuModel } from '../models/category-menu.model';
import { environment } from 'src/environments/environment';

const categoriesUrl = environment.api.baseUrl + 'categories/';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  constructor(private http: HttpClient) { }

  public getCategories$(): Observable<ICategoriesListModel[]> {
    return this.http.get<ICategoriesListModel[]>(categoriesUrl);
  }

  public getCategory$(id: string): Observable<ICategoryBindingModel> {
    return this.http.get<ICategoryBindingModel>(categoriesUrl + id);
  }

  public createCategory$(body: ICategoryBindingModel): Observable<void> {
    return this.http.post<void>(categoriesUrl, body);
  }

  public editCategory$(id: string, body: ICategoryBindingModel): Observable<void> {
    return this.http.put<void>(categoriesUrl + id, body);
  }

  public getCategoryNames$(): Observable<ICategoryMenuModel[]> {
    return this.http.get<ICategoryMenuModel[]>(categoriesUrl + 'menu');
  }
}
