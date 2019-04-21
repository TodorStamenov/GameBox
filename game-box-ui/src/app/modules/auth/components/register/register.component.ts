import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { Subscription } from 'rxjs';

import { AuthService } from '../../../../sharedServices/auth.service';
import { matchingProperties } from '../common/equal-value-validator';
import { FormService } from 'src/app/sharedServices/form.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnInit, OnDestroy {
  private subscription = new Subscription();

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
    private authService: AuthService,
    private formService: FormService
  ) { }

  public ngOnInit(): void {
    this.registerForm = this.fb.group({
      'username': new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]),
      'password': new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]),
      'repeatPassword': new FormControl('')
    }, { validator: matchingProperties('password', 'repeatPassword') });

    const usernameControl = this.registerForm.controls.username;
    this.subscription.add(usernameControl
      .valueChanges
      .subscribe(() => {
        this.usernameMessage = '';
        this.usernameMessage = this.formService.setMessage(usernameControl, 'usernameValidationMessage', this.validationMessages);
      }));

    const passwordControl = this.registerForm.controls.password;
    this.subscription.add(passwordControl
      .valueChanges
      .subscribe(() => {
        this.passwordMessage = '';
        this.passwordMessage = this.formService.setMessage(passwordControl, 'passwordValidationMessage', this.validationMessages);
      }));

    const repeatPasswordControl = this.registerForm.controls.repeatPassword;
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

  public register(): void {
    this.authService
      .register(this.registerForm.value)
      .subscribe();
  }
}
