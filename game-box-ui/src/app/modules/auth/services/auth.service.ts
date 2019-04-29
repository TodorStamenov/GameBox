import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ToastrService } from 'ngx-toastr';

import { IRegisterModel } from '../../auth/models/register.model';
import { ILoginModel } from '../../auth/models/login.model';
import { IChangePasswordModel } from '../../auth/models/change-password.model';
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

  public register(body: IRegisterModel) {
    return this.http.post(authUrl + 'register', body);
  }

  public login(body: ILoginModel) {
    return this.http.post(authUrl + 'login', body);
  }

  public logout(): void {
    localStorage.clear();
    this.toastr.success('You have successfully logged out', 'Success!');
  }

  public changePassword(body: IChangePasswordModel) {
    return this.http.post(authUrl + 'changePassword', body);
  }
}
