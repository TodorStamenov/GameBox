import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';

import { AuthService } from '../../../../services/auth.service';
import { FormService } from 'src/app/services/form.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnInit {
  public registerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthService,
    public formService: FormService
  ) { }

  get username(): AbstractControl {
    return this.registerForm.get('username');
  }

  get password(): AbstractControl {
    return this.registerForm.get('password');
  }

  get repeatPassword(): AbstractControl {
    return this.registerForm.get('repeatPassword');
  }

  public ngOnInit(): void {
    this.registerForm = this.fb.group({
      'username': [null, [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      'password': [null, [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      'repeatPassword': [null]
    });
  }

  public register(): void {
    this.authService.register$(this.registerForm.value)
      .subscribe(() => this.router.navigate(['/']));
  }
}
