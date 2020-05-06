import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { IUsersListModel } from '../modules/user/models/users-list.model';
import { ICreateUserModel } from '../modules/user/models/create-user.model';
import { environment } from 'src/environments/environment';

const usersUrl = environment.api.baseUrl + 'users/';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) { }

  public getUsers$(username: string): Observable<IUsersListModel[]> {
    return this.http.get<IUsersListModel[]>(usersUrl + 'all?username=' + username);
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

  public createUser$(body: ICreateUserModel): Observable<void> {
    return this.http.post<void>(usersUrl + 'create', body);
  }
}
