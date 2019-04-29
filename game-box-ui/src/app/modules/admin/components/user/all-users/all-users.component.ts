import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

import { Observable, Subscription } from 'rxjs';

import { UserModel } from '../../../models/users/user.model';
import { AdminService } from '../../../services/admin.service';
import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-all-users',
  templateUrl: './all-users.component.html'
})
export class AllUsersComponent implements OnInit, OnDestroy {
  private subscription = new Subscription();

  public searchForm: FormGroup;
  public users$: Observable<UserModel[]>;

  get username() { return this.searchForm.get('username'); }

  constructor(
    private fb: FormBuilder,
    private adminService: AdminService
  ) { }

  public ngOnInit() {
    this.getUsers();

    this.searchForm = this.fb.group({
      'username': [null]
    });

    this.subscription.add(this.username
      .valueChanges.pipe(
        debounceTime(500)
      )
      .subscribe(() => this.getUsers(this.username.value)));
  }

  public ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  public getUsers(username?: string): void {
    this.users$ = this.adminService.getUsers$(username || '');
  }

  public lock(username: string): void {
    this.adminService
      .lock$(username)
      .subscribe(() => this.getUsers(this.username.value));
  }

  public unlock(username: string): void {
    this.adminService
      .unlock$(username)
      .subscribe(() => this.getUsers(this.username.value));
  }

  public addRole(username: string, roleName: string): void {
    this.adminService
      .addRole$(username, roleName)
      .subscribe(() => this.getUsers(this.username.value));
  }

  public removeRole(username: string, roleName: string): void {
    this.adminService
      .removeRole$(username, roleName)
      .subscribe(() => this.getUsers(this.username.value));
  }
}
