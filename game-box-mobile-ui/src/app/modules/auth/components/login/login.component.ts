import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, AbstractControl, FormControl } from '@angular/forms';

import { AuthService } from '../../../../services/auth.service';
import { UIService } from '~/app/services/ui.service';
import * as utils from 'tns-core-modules/utils/utils';

@Component({
  selector: 'ns-login',
  templateUrl: './login.component.html',
  moduleId: module.id
})
export class LoginComponent implements OnInit {
  public loading = false;
  public loginForm: FormGroup;

  constructor(
    private authService: AuthService,
    private uiService: UIService
  ) { }

  get username(): AbstractControl {
    return this.loginForm.get('username');
  }

  get password(): AbstractControl {
    return this.loginForm.get('password');
  }

  public ngOnInit(): void {
    this.loginForm = new FormGroup({
      'username': new FormControl(null, {
        updateOn: 'change',
        validators: [Validators.required, Validators.minLength(3)]
      }),
      'password': new FormControl(null, {
        updateOn: 'change',
        validators: [Validators.required, Validators.minLength(3)]
      })
    });
  }

  public onLogin(): void {
    if (this.username.invalid) {
      this.uiService.showMessage('Username should be at least 3 symbols');
      return;
    }

    if (this.password.invalid) {
      this.uiService.showMessage('Password should be at least 3 symbols');
      return;
    }

    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    this.authService.login(this.loginForm.value)
      .subscribe(
        () => this.uiService.navigate('/'),
        () => this.loading = false
      );
  }

  public onFormTap(): void {
    utils.ad.dismissSoftInput();
  }
}
