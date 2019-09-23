import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Store } from '@ngrx/store';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { constants } from '../../../common';
import { IUsersListModel } from '../models/users-list.model';
import { ICreateUserModel } from '../models/create-user.model';
import { IAppState } from 'src/app/store/app.state';
import { GetAllUsers } from 'src/app/store/users/users.actions';

const usersUrl = constants.host + 'users/';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(
    private http: HttpClient,
    private store: Store<IAppState>
  ) { }

  public getUsers$(username: string): Observable<void> {
    return this.http.get<IUsersListModel[]>(usersUrl + 'all?username=' + username).pipe(
      map((users: IUsersListModel[]) => {
        this.store.dispatch(new GetAllUsers(users));
      })
    );
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
