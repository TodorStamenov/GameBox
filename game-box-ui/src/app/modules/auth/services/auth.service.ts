import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ToastrService } from 'ngx-toastr';

import { RegisterModel } from '../../auth/models/register.model';
import { LoginModel } from '../../auth/models/login.model';
import { ChangePasswordModel } from '../../auth/models/change-password.model';
import { constants } from '../../../common';

const authUrl = constants.host + 'account/';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    private http: HttpClient,
    private toastr: ToastrService
  ) { }

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
}
