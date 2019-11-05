import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, AbstractControl, FormControl } from '@angular/forms';

import { RouterExtensions } from 'nativescript-angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'ns-login',
  templateUrl: './login.component.html',
  moduleId: module.id
})
export class LoginComponent implements OnInit {
  public loading = false;
  public loginForm: FormGroup;

  get username(): AbstractControl {
    return this.loginForm.get('username');
  }

  get password(): AbstractControl {
    return this.loginForm.get('password');
  }

  constructor(
    private authService: AuthService,
    private router: RouterExtensions
  ) { }

  public ngOnInit(): void {
    this.loginForm = new FormGroup({
      'username': new FormControl(null, {
        updateOn: 'blur',
        validators: [Validators.required, Validators.minLength(3)]
      }),
      'password': new FormControl(null, {
        updateOn: 'blur',
        validators: [Validators.required, Validators.minLength(3)]
      })
    });
  }

  public onLogin(): void {
    if (this.loginForm.invalid) {
      return;
    }

    this.authService
      .login(this.loginForm.value)
      .subscribe(() => {
        this.router.navigate(['/'], {
          clearHistory: true,
          transition: { name: 'slideLeft' }
        });
      });
  }
}
