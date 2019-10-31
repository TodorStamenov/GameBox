import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AuthService } from '../../services/auth.service';
import { FormService } from 'src/app/modules/core/services/form.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnInit {
  public registerForm: FormGroup;

  get username() { return this.registerForm.get('username'); }
  get password() { return this.registerForm.get('password'); }
  get repeatPassword() { return this.registerForm.get('repeatPassword'); }

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    public formService: FormService
  ) { }

  public ngOnInit(): void {
    this.registerForm = this.fb.group({
      'username': [null, [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      'password': [null, [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      'repeatPassword': [null]
    });
  }

  public register(): void {
    this.authService
      .register(this.registerForm.value)
      .subscribe();
  }
}
