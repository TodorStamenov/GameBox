import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';

import { Subscription } from 'rxjs';

import { AuthService } from '../../../../sharedServices/auth.service';
import { FormService } from 'src/app/sharedServices/form.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit, OnDestroy {
  private usernameSubscription: Subscription;
  private passwordSubscription: Subscription;

  public loginForm: FormGroup;

  public usernameMessage: string;
  public passwordMessage: string;

  private validationMessages = {
    usernameValidationMessage: {
      required: 'Username is required',
      minlength: 'Username should be at least 3 symbols long!'
    },
    passwordValidationMessage: {
      required: 'Password is required',
      minlength: 'Password should be at least 3 symbols long!'
    }
  };

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private formService: FormService
  ) { }

  public ngOnInit(): void {
    this.loginForm = this.fb.group({
      'username': new FormControl('', [Validators.required, Validators.minLength(3)]),
      'password': new FormControl('', [Validators.required, Validators.minLength(3)])
    });

    const usernameControl = this.loginForm.controls.username;
    this.usernameSubscription = usernameControl
      .valueChanges
      .subscribe(() => {
        this.usernameMessage = '';
        this.usernameMessage = this.formService.setMessage(usernameControl, 'usernameValidationMessage', this.validationMessages);
      });

    const passwordControl = this.loginForm.controls.password;
    this.passwordSubscription = passwordControl
      .valueChanges
      .subscribe(() => {
        this.passwordMessage = '';
        this.passwordMessage = this.formService.setMessage(passwordControl, 'passwordValidationMessage', this.validationMessages);
      });
  }

  public ngOnDestroy(): void {
    this.usernameSubscription.unsubscribe();
    this.passwordSubscription.unsubscribe();
  }

  public login(): void {
    this.authService
      .login(this.loginForm.value)
      .subscribe();
  }
}
