import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { constants } from '../../../common';
import { ILoginModel } from '../models/login.model';
import { IRegisterModel } from '../models/register.model';

const authUrl = constants.host + 'account/';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    private http: HttpClient
  ) { }

  public register(body: IRegisterModel): Observable<void> {
    return this.http.post<void>(authUrl + 'register', body);
  }

  public login(body: ILoginModel): Observable<void> {
    return this.http.post<void>(authUrl + 'login', body);
  }

  public logout(): void {

  }
}
