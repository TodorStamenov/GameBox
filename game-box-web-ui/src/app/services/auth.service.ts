import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Store } from '@ngrx/store';

import { Observable } from 'rxjs';
import { take, tap } from 'rxjs/operators';

import { IRegisterModel } from '../modules/auth/models/register.model';
import { ILoginModel } from '../modules/auth/models/login.model';
import { IChangePasswordModel } from '../modules/auth/models/change-password.model';
import { IUser } from '../modules/auth/models/user.model';
import { environment } from 'src/environments/environment';
import { IAppState } from 'src/app/store/app.state';
import { ToastType } from '../modules/core/enums/toast-type.enum';
import { DisplayToastMessage } from 'src/app/store/+store/core/core.actions';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authUrl = environment.api.gameBoxApiUrl + 'account/';
  private currentUser: IUser;

  constructor(
    private http: HttpClient,
    private store: Store<IAppState>
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

  public login$(body: ILoginModel): Observable<IUser> {
    return this.http.post<IUser>(this.authUrl + 'login', body).pipe(
      take(1),
      tap(user => this.user = user)
    );
  }

  public register$(body: IRegisterModel): Observable<IUser> {
    return this.http.post<IUser>(this.authUrl + 'register', body).pipe(
      take(1),
      tap(user => this.user = user)
    );
  }

  public changePassword$(body: IChangePasswordModel): Observable<void> {
    return this.http.post<void>(this.authUrl + 'changePassword', body).pipe(take(1));
  }

  public logout(): void {
    this.currentUser = null;
    localStorage.clear();
    this.store.dispatch(new DisplayToastMessage({
      message: 'Logout successful',
      toastType: ToastType.success
    }));
  }
}
