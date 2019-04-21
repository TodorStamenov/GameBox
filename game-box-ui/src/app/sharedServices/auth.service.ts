import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ToastrService } from 'ngx-toastr';

import { RegisterModel } from '../modules/auth/models/register.model';
import { LoginModel } from '../modules/auth/models/login.model';
import { ChangePasswordModel } from '../modules/auth/models/change-password.model';
import { constants } from '../common';

const authUrl = constants.host + 'account/';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    private http: HttpClient,
    private toastr: ToastrService
  ) { }

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
    this.toastr.success('You have successfully logged out', 'Success!');
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
