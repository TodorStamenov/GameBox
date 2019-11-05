import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';

import { FormService } from 'src/app/modules/core/services/form.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html'
})
export class ChangePasswordComponent implements OnInit {
  public changePasswordForm: FormGroup;

  get oldPassword(): AbstractControl {
    return this.changePasswordForm.get('oldPassword');
  }

  get newPassword(): AbstractControl {
    return this.changePasswordForm.get('newPassword');
  }

  get repeatPassword(): AbstractControl {
    return this.changePasswordForm.get('repeatPassword');
  }

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    public formService: FormService
  ) { }

  public ngOnInit(): void {
    this.changePasswordForm = this.fb.group({
      'oldPassword': [null, [Validators.required]],
      'newPassword': [null, [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      'repeatPassword': [null]
    });
  }

  public changePassword(): void {
    this.authService
      .changePassword(this.changePasswordForm.value)
      .subscribe(() => this.router.navigate(['/']));
  }
}
