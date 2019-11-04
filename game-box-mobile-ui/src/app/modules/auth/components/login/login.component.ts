import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'ns-login',
  templateUrl: './login.component.html',
  moduleId: module.id
})
export class LoginComponent implements OnInit {
  public loginForm: FormGroup;

  get username() { return this.loginForm.get('username'); }
  get password() { return this.loginForm.get('password'); }

  constructor(
    private fb: FormBuilder,
    private authService: AuthService
  ) { }

  public ngOnInit(): void {
    this.loginForm = this.fb.group({
      'username': [null, [Validators.required, Validators.minLength(3)]],
      'password': [null, [Validators.required, Validators.minLength(3)]]
    });
  }

  public login(): void {
    this.authService.login(this.loginForm.value)
      .subscribe();
  }
}
