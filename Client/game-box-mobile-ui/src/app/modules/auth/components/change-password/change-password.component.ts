import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, AbstractControl, FormControl } from '@angular/forms';
import { RouterExtensions } from '@nativescript/angular';
import { Page, Utils } from '@nativescript/core';

import { AuthService } from '../../../../services/auth.service';
import { UIService } from '~/app/services/ui.service';

@Component({
  selector: 'ns-change-password',
  templateUrl: './change-password.component.html',
  moduleId: module.id
})
export class ChangePasswordComponent implements OnInit {
  public loading = false;
  public changePasswordForm: FormGroup;

  constructor(
    private authService: AuthService,
    private router: RouterExtensions,
    private uiService: UIService
  ) { }

  get oldPassword(): AbstractControl {
    return this.changePasswordForm.get('oldPassword');
  }

  get newPassword(): AbstractControl {
    return this.changePasswordForm.get('newPassword');
  }

  get repeatPassword(): AbstractControl {
    return this.changePasswordForm.get('repeatPassword');
  }

  public ngOnInit(): void {
    this.changePasswordForm = new FormGroup({
      'oldPassword': new FormControl(null, {
        updateOn: 'change',
        validators: [Validators.required, Validators.minLength(3)]
      }),
      'newPassword': new FormControl(null, {
        updateOn: 'change',
        validators: [Validators.required, Validators.minLength(3)]
      }),
      'repeatPassword': new FormControl(null, {
        updateOn: 'change',
        validators: [Validators.required, Validators.minLength(3)]
      })
    });
  }

  public onChangePassword(): void {
    if (this.oldPassword.invalid) {
      this.uiService.showMessage('Old Password should be at least 3 symbols');
      return;
    }

    if (this.newPassword.invalid) {
      this.uiService.showMessage('New Password should be at least 3 symbols');
      return;
    }

    if (this.repeatPassword.value !== this.newPassword.value) {
      this.uiService.showMessage('New Password and Repeat password values should match');
      return;
    }

    if (this.changePasswordForm.invalid) {
      return;
    }

    this.loading = true;
    this.authService.changePassword$(this.changePasswordForm.value)
      .subscribe(
        () => this.router.backToPreviousPage(),
        () => this.loading = false
      );
  }

  public onFormTap(): void {
    Utils.ad.dismissSoftInput();
  }
}
