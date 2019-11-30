import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';

import { UserService } from '../../services/user.service';
import { FormService } from 'src/app/modules/core/services/form.service';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html'
})
export class CreateUserComponent implements OnInit {
  public createUserForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router,
    public formService: FormService
  ) { }

  get username(): AbstractControl {
    return this.createUserForm.get('username');
  }

  get password(): AbstractControl {
    return this.createUserForm.get('password');
  }

  get repeatPassword(): AbstractControl {
    return this.createUserForm.get('repeatPassword');
  }

  public ngOnInit(): void {
    this.createUserForm = this.fb.group({
      'username': [null, [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      'password': [null, [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      'repeatPassword': [null]
    });
  }

  public createUser(): void {
    this.userService.createUser$(this.createUserForm.value)
      .subscribe(() => this.router.navigate(['/users/all']));
  }
}
