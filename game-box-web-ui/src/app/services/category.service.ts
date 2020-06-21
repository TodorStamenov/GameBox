import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

import { ICategoriesListModel } from '../modules/category/models/categories-list.model';
import { ICategoryBindingModel } from '../modules/category/models/category-binding.model';
import { ICategoryMenuModel } from '../modules/category/models/category-menu.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private categoriesUrl = environment.api.gameBoxApiUrl + 'categories/';

  constructor(private http: HttpClient) { }

  public getCategories$(): Observable<ICategoriesListModel[]> {
    return this.http.get<ICategoriesListModel[]>(this.categoriesUrl).pipe(take(1));
  }

  public getCategory$(id: string): Observable<ICategoryBindingModel> {
    return this.http.get<ICategoryBindingModel>(this.categoriesUrl + id).pipe(take(1));
  }

  public createCategory$(body: ICategoryBindingModel): Observable<void> {
    return this.http.post<void>(this.categoriesUrl, body).pipe(take(1));
  }

  public editCategory$(id: string, body: ICategoryBindingModel): Observable<void> {
    return this.http.put<void>(this.categoriesUrl + id, body).pipe(take(1));
  }

  public getCategoryNames$(): Observable<ICategoryMenuModel[]> {
    return this.http.get<ICategoryMenuModel[]>(this.categoriesUrl + 'menu').pipe(take(1));
  }
}
