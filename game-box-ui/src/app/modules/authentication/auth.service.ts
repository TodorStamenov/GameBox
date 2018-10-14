import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RegisterModel } from './models/register.model';
import { LoginModel } from './models/login.model';
import { constants } from '../../common';

const authUrl = constants.host + 'account/';

@Injectable()
export class AuthService {
  constructor(private http: HttpClient) { }

  get user() {
    return JSON.parse(localStorage.getItem('currentUser'));
  }

  register(body: RegisterModel) {
    return this.http.post(authUrl + 'register', body);
  }

  login(body: LoginModel) {
    return this.http.post(authUrl + 'login', body);
  }

  logout(): void {
    localStorage.clear();
  }

  isAuthenticated(): boolean {
    return localStorage.getItem('currentUser') !== null;
  }

  isAdmin(): boolean {
    if (this.user) {
      return this.user.isAdmin;
    }

    return false;
  }
}