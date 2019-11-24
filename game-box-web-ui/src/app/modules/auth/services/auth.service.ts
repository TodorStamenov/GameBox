import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { ToastrService } from 'ngx-toastr';
import { IRegisterModel } from '../../auth/models/register.model';
import { ILoginModel } from '../../auth/models/login.model';
import { IChangePasswordModel } from '../../auth/models/change-password.model';
import { constants } from '../../../common';
import { IUser } from '../models/user.model';

const authUrl = constants.apiHost + 'account/';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUser: IUser;

  constructor(
    private http: HttpClient,
    private toastrService: ToastrService
  ) { }

  get user(): IUser {
    return this.currentUser || JSON.parse(localStorage.getItem('currentUser'));
  }

  set user(user: IUser) {
    this.currentUser = user;
    localStorage.setItem('currentUser', JSON.stringify(this.currentUser));
  }

  get isAuthed(): boolean {
    if (!this.user || !this.user.expirationDate) {
      return false;
    }

    return Date.now() < new Date(this.user.expirationDate).getTime();
  }

  get isAdmin(): boolean {
    return this.isAuthed && this.user.isAdmin;
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

  public changePassword(body: IChangePasswordModel): Observable<void> {
    return this.http.post<void>(authUrl + 'changePassword', body);
  }

  public logout(): void {
    this.currentUser = null;
    localStorage.clear();
    this.toastrService.success('Logout successful', 'Success!');
  }
}
