import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ListCategoriesModel } from '../models/categories/list-categories.model';
import { constants } from '../../../common';
import { CategoryBindingModel } from '../models/categories/category-binding.model';
import { CategoryMenuModel } from '../models/categories/category-menu.model';

const categoriesUrl = constants.host + 'categories/';

@Injectable()
export class CategoryService {
  constructor(private http: HttpClient) { }

  getCategories() {
    return this.http.get<ListCategoriesModel[]>(categoriesUrl);
  }

  getCategory(id: string) {
    return this.http.get<ListCategoriesModel>(categoriesUrl + id);
  }

  createCategory(body: CategoryBindingModel) {
    return this.http.post<CategoryBindingModel>(categoriesUrl, body);
  }

  editCategory(id: string, body: CategoryBindingModel) {
    return this.http.put(categoriesUrl + id, body);
  }

  getCategoryNames() {
    return this.http.get<CategoryMenuModel[]>(categoriesUrl + 'menu')
  }
}