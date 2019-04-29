import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { constants } from '../../../common';
import { IUserModel } from '../models/users/user.model';
import { ICreateUserModel } from '../models/users/create-user.model';

const usersUrl = constants.host + 'users/';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  constructor(private http: HttpClient) { }

  public getUsers$(username: string): Observable<IUserModel[]> {
    return this.http.get<IUserModel[]>(usersUrl + 'all?username=' + username);
  }

  public lock$(username: string): Observable<string> {
    return this.http.get<string>(usersUrl + 'lock?username=' + username);
  }

  public unlock$(username: string): Observable<string> {
    return this.http.get<string>(usersUrl + 'unlock?username=' + username);
  }

  public addRole$(username: string, role: string): Observable<string> {
    return this.http.get<string>(usersUrl + 'addRole?username=' + username + '&roleName=' + role);
  }

  public removeRole$(username: string, role: string): Observable<string> {
    return this.http.get<string>(usersUrl + 'removeRole?username=' + username + '&roleName=' + role);
  }

  public createUser$(body: ICreateUserModel): Observable<any> {
    return this.http.post(usersUrl + 'create', body);
  }
}
