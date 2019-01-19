import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { RegisterModel } from '../modules/authentication/models/register.model';
import { LoginModel } from '../modules/authentication/models/login.model';
import { constants } from '../common';
import { ChangePasswordModel } from '../modules/authentication/models/change-password.model';

const authUrl = constants.host + 'account/';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient) { }

  get user() {
    return JSON.parse(localStorage.getItem('currentUser'));
  }

  public register(body: RegisterModel) {
    return this.http.post(authUrl + 'register', body);
  }

  public login(body: LoginModel) {
    return this.http.post(authUrl + 'login', body);
  }

  public logout(): void {
    localStorage.clear();
  }

  public changePassword(body: ChangePasswordModel) {
    return this.http.post(authUrl + 'changePassword', body);
  }

  public isAuthenticated(): boolean {
    return localStorage.getItem('currentUser') !== null;
  }

  public isAdmin(): boolean {
    if (this.user) {
      return this.user.isAdmin;
    }

    return false;
  }
}
