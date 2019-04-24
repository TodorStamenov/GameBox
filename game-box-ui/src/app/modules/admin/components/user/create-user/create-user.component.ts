import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators, FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { FormService } from 'src/app/modules/core/sharedServices/form.service';
import { matchingProperties } from '../../../../auth/components/common/equal-value-validator';
import { AdminService } from '../../../services/admin.service';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html'
})
export class CreateUserComponent implements OnInit, OnDestroy {
  private subscription = new Subscription();

  public createUserForm: FormGroup;

  public usernameMessage: string;
  public passwordMessage: string;
  public repeatPasswordMessage: string;

  private validationMessages = {
    usernameValidationMessage: {
      required: 'Username is required!',
      minlength: 'Username should be at least 3 symbols long!',
      maxlength: 'Username should be less than 50 symbols long!'
    },
    passwordValidationMessage: {
      required: 'Password is required',
      minlength: 'Password should be at least 3 symbols long!',
      maxlength: 'Password should be less than 50 symbols long!'
    }
  };

  constructor(
    private fb: FormBuilder,
    private adminService: AdminService,
    private formService: FormService,
    private router: Router
  ) { }

  public ngOnInit(): void {
    this.createUserForm = this.fb.group({
      'username': new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]),
      'password': new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]),
      'repeatPassword': new FormControl('')
    }, { validator: matchingProperties('password', 'repeatPassword') });

    const usernameControl = this.createUserForm.controls.username;
    this.subscription.add(usernameControl
      .valueChanges
      .subscribe(() => {
        this.usernameMessage = '';
        this.usernameMessage = this.formService.setMessage(usernameControl, 'usernameValidationMessage', this.validationMessages);
      }));

    const passwordControl = this.createUserForm.controls.password;
    this.subscription.add(passwordControl
      .valueChanges
      .subscribe(() => {
        this.passwordMessage = '';
        this.passwordMessage = this.formService.setMessage(passwordControl, 'passwordValidationMessage', this.validationMessages);
      }));

    const repeatPasswordControl = this.createUserForm.controls.repeatPassword;
    this.subscription.add(repeatPasswordControl
      .valueChanges
      .subscribe(() => {
        this.repeatPasswordMessage = '';
        this.repeatPasswordMessage = this.formService
          .setPasswordMessage(repeatPasswordControl, passwordControl, 'Password', 'Repeat Password');
      }));
  }

  public ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  public createUser(): void {
    this.adminService
      .createUser$(this.createUserForm.value)
      .subscribe(() => this.router.navigate(['/admin/users/all']));
  }
}
