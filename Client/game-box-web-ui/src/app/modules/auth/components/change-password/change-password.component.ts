import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, Validators, UntypedFormGroup, AbstractControl } from '@angular/forms';

import { AuthService } from '../../../../services/auth.service';
import { FormService } from 'src/app/services/form.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html'
})
export class ChangePasswordComponent implements OnInit {
  public changePasswordForm: UntypedFormGroup;

  constructor(
    private fb: UntypedFormBuilder,
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

  public getField(name: string): AbstractControl {
    return this.changePasswordForm.get(name);
  }

  public changePassword(): void {
    this.authService.changePassword$(this.changePasswordForm.value)
      .subscribe(() => this.router.navigate(['/']));
  }
}
