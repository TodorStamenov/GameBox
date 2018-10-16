import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CategoryModel } from '../models/categories/category.model';
import { constants } from '../../../common';
import { CreateCategoryModel } from '../models/categories/create-category.model';

const categoriesUrl = constants.host + 'categories/';

@Injectable()
export class CategoryService {
  constructor(private http: HttpClient) { }

  getCategories() {
    return this.http.get<CategoryModel[]>(categoriesUrl);
  }

  getCategory(id: string){
    return this.http.get<CategoryModel>(categoriesUrl + id);
  }

  createCategory(body: CreateCategoryModel) {
    return this.http.post<CreateCategoryModel>(categoriesUrl, body);
  }

  editCategory(id: string, body: CreateCategoryModel){
    return this.http.put(categoriesUrl + id, body);
  }
}