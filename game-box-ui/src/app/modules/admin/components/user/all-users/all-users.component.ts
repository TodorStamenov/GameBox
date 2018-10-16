import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { UserModel } from '../../../models/users/user.model';
import { AdminService } from '../../../services/admin.service';

@Component({
  selector: 'app-user',
  templateUrl: './all-users.component.html'
})
export class AllUsersComponent implements OnInit {
  public searchForm: FormGroup;
  public users: UserModel[];

  constructor(
    private fb: FormBuilder,
    private adminService: AdminService
  ) { }

  ngOnInit() {
    this.getUsers('');

    this.searchForm = this.fb.group({
      username: new FormControl('')
    });

    const usernameControl = this.searchForm.controls.username;
    usernameControl
      .valueChanges
      .subscribe(() => this.getUsers(usernameControl.value));
  }

  getUsers(username: string){
    this.adminService
      .getUsers(username)
      .subscribe(res => this.users = res);
  }

  lock(username: string) {
    this.adminService
      .lock(username)
      .subscribe(() => this.getUsers(this.searchForm.controls.username.value));
  }

  unlock(username: string) {
    this.adminService
      .unlock(username)
      .subscribe(() => this.getUsers(this.searchForm.controls.username.value));
  }

  addRole(username: string, roleName: string) {
    this.adminService
      .addRole(username, roleName)
      .subscribe(() => this.getUsers(this.searchForm.controls.username.value));
  }

  removeRole(username: string, roleName: string) {
    this.adminService
      .removeRole(username, roleName)
      .subscribe(() => this.getUsers(this.searchForm.controls.username.value));
  }
}