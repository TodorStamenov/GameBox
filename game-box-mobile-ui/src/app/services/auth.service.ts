import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { constants } from '../common';
import { ILoginModel } from '../modules/auth/models/login.model';
import { IRegisterModel } from '../modules/auth/models/register.model';
import { IUser } from '../modules/auth/models/user.mode';
import { UIService } from './ui.service';
import { IChangePasswordModel } from '../modules/auth/models/change-password.model';
import * as cacheService from 'tns-core-modules/application-settings';

const authUrl = constants.host + 'account/';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUser: IUser;

  constructor(
    private http: HttpClient,
    private uiService: UIService
  ) { }

  get user(): IUser {
    return this.currentUser ||
      (cacheService.hasKey('currentUser') && JSON.parse(cacheService.getString('currentUser')));
  }

  set user(user: IUser) {
    this.currentUser = user;
    cacheService.setString('currentUser', JSON.stringify(this.currentUser));
  }

  get isAuthed(): boolean {
    if (!this.user || !this.user.expirationDate) {
      return false;
    }

    return Date.now() < new Date(this.user.expirationDate).getTime();
  }

  public login(body: ILoginModel): Observable<IUser> {
    return this.http.post<IUser>(authUrl + 'login', body).pipe(
      tap(user => this.user = user)
    );
  }

  public register(body: IRegisterModel): Observable<IUser> {
    return this.http.post<IUser>(authUrl + 'register', body).pipe(
      tap(user => this.user = user)
    );
  }

  public changePassword(body: IChangePasswordModel): Observable<any> {
    return this.http.post<any>(authUrl + 'changePassword', body);
  }

  public logout(): void {
    this.currentUser = null;
    cacheService.clear();
    this.uiService.showMessage('Logout successful');
  }
}
