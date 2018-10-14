import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators, AbstractControl } from '@angular/forms';
import { AuthService } from '../../auth.service';
import { matchingProperties } from './equal-value-validator';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnInit {
  public registerForm: FormGroup;

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
    private authService: AuthService
  ) { }

  ngOnInit() {
    this.registerForm = this.fb.group({
      username: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]),
      password: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]),
      repeatPassword: new FormControl('')
    }, { validator: matchingProperties('password', 'repeatPassword') });

    const usernameControl = this.registerForm.controls.username;
    usernameControl
      .valueChanges
      .subscribe(() => {
        this.usernameMessage = '';
        this.usernameMessage = this.setMessage(usernameControl, 'usernameValidationMessage');
      });

    const passwordControl = this.registerForm.controls.password;
    passwordControl
      .valueChanges
      .subscribe(() => {
        this.passwordMessage = '';
        this.passwordMessage = this.setMessage(passwordControl, 'passwordValidationMessage');
      });

    const repeatPasswordControl = this.registerForm.controls.repeatPassword;
    repeatPasswordControl
      .valueChanges
      .subscribe(() => {
        this.repeatPasswordMessage = '';
        this.repeatPasswordMessage = this.setPasswordMessage(repeatPasswordControl, passwordControl, 'Password', 'Repeat Password');
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
      return `${fieldName} and ${targetFieldName} fields should have same value!`
    }
  }

  register(): void {
    this.authService
      .register(this.registerForm.value)
      .subscribe();
  }
}