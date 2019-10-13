import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';

import { IUsersListModel } from '../../models/users-list.model';
import { UserService } from '../../services/user.service';
import { IAppState } from 'src/app/store/app.state';
import { LoadAllUsers } from 'src/app/store/users/users.actions';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html'
})
export class UsersListComponent implements OnInit {
  private username = '';
  public users$: Observable<IUsersListModel[]>;

  constructor(
    private userService: UserService,
    private store: Store<IAppState>
  ) { }

  public ngOnInit(): void {
    this.getUsers();
    this.users$ = this.store.pipe(
      select(s => s.users.all)
    );
  }

  public setUsername(username: string): void {
    this.username = username;
    this.getUsers(username);
  }

  public lock(username: string): void {
    this.userService
      .lock$(username)
      .subscribe(() => this.getUsers(this.username));
  }

  public unlock(username: string): void {
    this.userService
      .unlock$(username)
      .subscribe(() => this.getUsers(this.username));
  }

  public addRole(username: string, roleName: string): void {
    this.userService
      .addRole$(username, roleName)
      .subscribe(() => this.getUsers(this.username));
  }

  public removeRole(username: string, roleName: string): void {
    this.userService
      .removeRole$(username, roleName)
      .subscribe(() => this.getUsers(this.username));
  }

  private getUsers(username: string = ''): void {
    this.store.dispatch(new LoadAllUsers(username));
  }
}
