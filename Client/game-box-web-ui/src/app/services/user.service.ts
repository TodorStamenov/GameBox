import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

import { IUsersListModel } from '../modules/user/models/users-list.model';
import { ICreateUserModel } from '../modules/user/models/create-user.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private usersUrl = environment.api.usersApiUrl + 'users/';

  constructor(private http: HttpClient) { }

  public getUsers$(username: string): Observable<IUsersListModel[]> {
    return this.http.get<IUsersListModel[]>(this.usersUrl + 'all?username=' + username).pipe(take(1));
  }

  public lock$(username: string): Observable<string> {
    return this.http.get<string>(this.usersUrl + 'lock?username=' + username).pipe(take(1));
  }

  public unlock$(username: string): Observable<string> {
    return this.http.get<string>(this.usersUrl + 'unlock?username=' + username).pipe(take(1));
  }

  public addRole$(username: string, role: string): Observable<string> {
    return this.http.get<string>(this.usersUrl + 'add-role?username=' + username + '&roleName=' + role).pipe(take(1));
  }

  public removeRole$(username: string, role: string): Observable<string> {
    return this.http.get<string>(this.usersUrl + 'remove-role?username=' + username + '&roleName=' + role).pipe(take(1));
  }

  public createUser$(body: ICreateUserModel): Observable<void> {
    return this.http.post<void>(this.usersUrl + 'create', body).pipe(take(1));
  }
}
