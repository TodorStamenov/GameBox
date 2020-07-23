import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';

import { IUsersListModel } from '../../models/users-list.model';
import { UserService } from '../../../../services/user.service';
import { LoadAllUsers } from 'src/app/modules/user/+state/users.actions';
import { IState } from '../../+state/users.state';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html'
})
export class UsersListComponent implements OnInit {
  private username = '';
  public users$: Observable<IUsersListModel[]>;

  constructor(
    private userService: UserService,
    private store: Store<IState>
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

  public onLock(username: string): void {
    this.userService
      .lock$(username)
      .subscribe(() => this.getUsers(this.username));
  }

  public onUnlock(username: string): void {
    this.userService
      .unlock$(username)
      .subscribe(() => this.getUsers(this.username));
  }

  public onAddRole(username: string, roleName: string): void {
    this.userService
      .addRole$(username, roleName)
      .subscribe(() => this.getUsers(this.username));
  }

  public onRemoveRole(username: string, roleName: string): void {
    this.userService
      .removeRole$(username, roleName)
      .subscribe(() => this.getUsers(this.username));
  }

  private getUsers(username: string = ''): void {
    this.store.dispatch(new LoadAllUsers(username));
  }
}
