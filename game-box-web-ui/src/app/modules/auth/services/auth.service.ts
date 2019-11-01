import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

import { IRegisterModel } from '../../auth/models/register.model';
import { ILoginModel } from '../../auth/models/login.model';
import { IChangePasswordModel } from '../../auth/models/change-password.model';
import { constants } from '../../../common';
import { AuthHelperService } from '../../core/services/auth-helper.service';

const authUrl = constants.host + 'account/';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    private http: HttpClient,
    private toastr: ToastrService,
    private authService: AuthHelperService
  ) { }

  public register(body: IRegisterModel): Observable<void> {
    return this.http.post<void>(authUrl + 'register', body);
  }

  public login(body: ILoginModel): Observable<void> {
    return this.http.post<void>(authUrl + 'login', body);
  }

  public logout(): void {
    this.authService.logout();
    this.toastr.success('You have successfully logged out', 'Success!');
  }

  public changePassword(body: IChangePasswordModel): Observable<void> {
    return this.http.post<void>(authUrl + 'changePassword', body);
  }
}
