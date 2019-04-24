import { Router } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormControl, Validators, FormGroup } from '@angular/forms';

import { Subscription } from 'rxjs';

import { AuthService } from '../../../core/sharedServices/auth.service';
import { matchingProperties } from '../common/equal-value-validator';
import { FormService } from 'src/app/modules/core/sharedServices/form.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html'
})
export class ChangePasswordComponent implements OnInit, OnDestroy {
  private subscription = new Subscription();

  public changePasswordForm: FormGroup;

  public oldPasswordMessage: string;
  public newPasswordMessage: string;
  public repeatPasswordMessage: string;

  private validationMessages = {
    oldPasswordValidationMessage: {
      required: 'Old Password is required!'
    },
    newPasswordValidationMessage: {
      required: 'New Password is required',
      minlength: 'New Password should be at least 3 symbols long!',
      maxlength: 'New Password should be less than 50 symbols long!'
    }
  };

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private formService: FormService
  ) { }

  public ngOnInit(): void {
    this.changePasswordForm = this.fb.group({
      'oldPassword': new FormControl('', [Validators.required]),
      'newPassword': new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]),
      'repeatPassword': new FormControl('')
    }, { validator: matchingProperties('newPassword', 'repeatPassword') });

    const oldPasswordControl = this.changePasswordForm.controls.oldPassword;
    this.subscription.add(oldPasswordControl
      .valueChanges
      .subscribe(() => {
        this.oldPasswordMessage = '';
        this.oldPasswordMessage = this.formService.setMessage(oldPasswordControl, 'oldPasswordValidationMessage', this.validationMessages);
      }));

    const newPasswordControl = this.changePasswordForm.controls.newPassword;
    this.subscription.add(newPasswordControl
      .valueChanges
      .subscribe(() => {
        this.newPasswordMessage = '';
        this.newPasswordMessage = this.formService.setMessage(newPasswordControl, 'newPasswordValidationMessage', this.validationMessages);
      }));

    const repeatPasswordControl = this.changePasswordForm.controls.repeatPassword;
    this.subscription.add(repeatPasswordControl
      .valueChanges
      .subscribe(() => {
        this.repeatPasswordMessage = '';
        this.repeatPasswordMessage = this.formService
          .setPasswordMessage(repeatPasswordControl, newPasswordControl, 'Old Password', 'Repeat Password');
      }));
  }

  public ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  public changePassword(): void {
    this.authService
      .changePassword(this.changePasswordForm.value)
      .subscribe(() => this.router.navigate(['/']));
  }
}
