import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';

import { Observable, Subscription } from 'rxjs';

import { UserModel } from '../../../models/users/user.model';
import { AdminService } from '../../../services/admin.service';

@Component({
  selector: 'app-user',
  templateUrl: './all-users.component.html'
})
export class AllUsersComponent implements OnInit, OnDestroy {
  private usernameSubscription: Subscription;

  public searchForm: FormGroup;
  public users$: Observable<UserModel[]>;

  constructor(
    private fb: FormBuilder,
    private adminService: AdminService
  ) { }

  public ngOnInit() {
    this.getUsers('');

    this.searchForm = this.fb.group({
      'username': new FormControl('')
    });

    const usernameControl = this.searchForm.controls.username;
    this.usernameSubscription = usernameControl
      .valueChanges
      .subscribe(() => this.getUsers(usernameControl.value));
  }

  public ngOnDestroy(): void {
    this.usernameSubscription.unsubscribe();
  }

  public getUsers(username: string): void {
    this.users$ = this.adminService.getUsers(username);
  }

  public lock(username: string): void {
    this.adminService
      .lock(username)
      .subscribe(() => this.getUsers(this.searchForm.controls.username.value));
  }

  public unlock(username: string): void {
    this.adminService
      .unlock(username)
      .subscribe(() => this.getUsers(this.searchForm.controls.username.value));
  }

  public addRole(username: string, roleName: string): void {
    this.adminService
      .addRole(username, roleName)
      .subscribe(() => this.getUsers(this.searchForm.controls.username.value));
  }

  public removeRole(username: string, roleName: string): void {
    this.adminService
      .removeRole(username, roleName)
      .subscribe(() => this.getUsers(this.searchForm.controls.username.value));
  }
}
