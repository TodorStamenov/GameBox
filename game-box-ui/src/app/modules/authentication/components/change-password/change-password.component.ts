import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, AbstractControl, Validators, FormGroup } from '@angular/forms';
import { AuthService } from '../../auth.service';
import { matchingProperties } from '../common/equal-value-validator';
import { Router } from '@angular/router';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html'
})
export class ChangePasswordComponent implements OnInit {
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
    private router: Router
  ) { }

  ngOnInit() {
    this.changePasswordForm = this.fb.group({
      oldPassword: new FormControl('', [Validators.required]),
      newPassword: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]),
      repeatPassword: new FormControl('')
    }, { validator: matchingProperties('newPassword', 'repeatPassword') });

    const oldPasswordControl = this.changePasswordForm.controls.oldPassword;
    oldPasswordControl
      .valueChanges
      .subscribe(() => {
        this.oldPasswordMessage = '';
        this.oldPasswordMessage = this.setMessage(oldPasswordControl, 'oldPasswordValidationMessage');
      });

    const newPasswordControl = this.changePasswordForm.controls.newPassword;
    newPasswordControl
      .valueChanges
      .subscribe(() => {
        this.newPasswordMessage = '';
        this.newPasswordMessage = this.setMessage(newPasswordControl, 'newPasswordValidationMessage');
      });

    const repeatPasswordControl = this.changePasswordForm.controls.repeatPassword;
    repeatPasswordControl
      .valueChanges
      .subscribe(() => {
        this.repeatPasswordMessage = '';
        this.repeatPasswordMessage = this.setPasswordMessage(repeatPasswordControl, newPasswordControl, 'Old Password', 'Repeat Password');
      });
  }

  setMessage(control: AbstractControl, messageKey: string): string {
    if ((control.touched || control.dirty) && control.errors) {
      return Object.keys(control.errors)
        .map(key => this.validationMessages[messageKey][key])
        .join(' ');
    }
  }

  setPasswordMessage(control: AbstractControl, targetControl: AbstractControl, fieldName: string, targetFieldName: string): string {
    if (control.value !== targetControl.value) {
      return `${fieldName} and ${targetFieldName} fields should have same value!`;
    }
  }

  changePassword(): void {
    this.authService
      .changePassword(this.changePasswordForm.value)
      .subscribe(() => this.router.navigate(['/']));
  }
}