import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';

import { IUsersListModel } from '../../../models/users/users-list.model';
import { AdminService } from '../../../services/admin.service';
import { IAppState } from 'src/app/store/app.state';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html'
})
export class UsersListComponent implements OnInit {
  private username = '';
  public users$: Observable<IUsersListModel[]>;

  constructor(
    private adminService: AdminService,
    private store: Store<IAppState>
  ) { }

  public ngOnInit(): void {
    this.getUsers();
  }

  public setUsername(username: string): void {
    this.username = username;
    this.getUsers(username);
  }

  public lock(username: string): void {
    this.adminService
      .lock$(username)
      .subscribe(() => this.getUsers(this.username));
  }

  public unlock(username: string): void {
    this.adminService
      .unlock$(username)
      .subscribe(() => this.getUsers(this.username));
  }

  public addRole(username: string, roleName: string): void {
    this.adminService
      .addRole$(username, roleName)
      .subscribe(() => this.getUsers(this.username));
  }

  public removeRole(username: string, roleName: string): void {
    this.adminService
      .removeRole$(username, roleName)
      .subscribe(() => this.getUsers(this.username));
  }

  private getUsers(username?: string): void {
    this.adminService
      .getUsers$(username || '')
      .subscribe(() => {
        this.users$ = this.store.pipe(
          select(state => state.users.all)
        );
      });
  }
}
