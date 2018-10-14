import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserModel } from './models/user.model';
import { CategoryModel } from './models/category.model';
import { constants } from '../../common';

const usersUrl = constants.host + 'users/';
const categoriesUrl = constants.host + 'categories/';

@Injectable()
export class AdminService {
  constructor(private http: HttpClient) { }

  getUsers(username: string) {
    return this.http.get<UserModel[]>(usersUrl + 'all?username=' + username);
  }

  lock(username: string) {
    return this.http.get<string>(usersUrl + 'lock?username=' + username);
  }

  unlock(username: string) {
    return this.http.get<string>(usersUrl + 'unlock?username=' + username);
  }

  addRole(username: string, role: string) {
    return this.http.get<string>(usersUrl + 'addRole?username=' + username + '&roleName=' + role);
  }

  removeRole(username: string, role: string) {
    return this.http.get<string>(usersUrl + 'removeRole?username=' + username + '&roleName=' + role);
  }

  getCategories() {
    return this.http.get<CategoryModel[]>(categoriesUrl + 'all');
  }
}