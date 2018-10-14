import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators, FormBuilder, AbstractControl } from '@angular/forms';
import { AuthService } from '../../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
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
    private authService: AuthService
  ) { }

  ngOnInit() {
    this.loginForm = this.fb.group({
      username: new FormControl('', [Validators.required, Validators.minLength(3)]),
      password: new FormControl('', [Validators.required, Validators.minLength(3)])
    });

    const usernameControl = this.loginForm.controls.username;
    usernameControl
      .valueChanges
      .subscribe(() => {
        this.usernameMessage = '';
        this.usernameMessage = this.setMessage(usernameControl, 'usernameValidationMessage');
      });

    const passwordControl = this.loginForm.controls.password;
    passwordControl
      .valueChanges
      .subscribe(() => {
        this.passwordMessage = '';
        this.passwordMessage = this.setMessage(passwordControl, 'passwordValidationMessage');
      });
  }

  setMessage(control: AbstractControl, messageKey: string): string {
    if ((control.touched || control.dirty) && control.errors) {
      return Object.keys(control.errors)
        .map(key => this.validationMessages[messageKey][key])
        .join(' ');
    }
  }

  login(): void {
    this.authService
      .login(this.loginForm.value)
      .subscribe();
  }
}