import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, AbstractControl, FormControl } from '@angular/forms';

import { AuthService } from '../../../../services/auth.service';
import { UIService } from '~/app/services/ui.service';
import * as utils from 'tns-core-modules/utils/utils';

@Component({
  selector: 'ns-register',
  templateUrl: './register.component.html',
  moduleId: module.id
})
export class RegisterComponent implements OnInit {
  public loading = false;
  public registerForm: FormGroup;

  constructor(
    private authService: AuthService,
    private uiService: UIService
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
    this.registerForm = new FormGroup({
      'username': new FormControl(null, {
        updateOn: 'change',
        validators: [Validators.required, Validators.minLength(3)]
      }),
      'password': new FormControl(null, {
        updateOn: 'change',
        validators: [Validators.required, Validators.minLength(3)]
      }),
      'repeatPassword': new FormControl(null, {
        updateOn: 'change',
        validators: [Validators.required, Validators.minLength(3)]
      })
    });
  }

  public onRegister(): void {
    if (this.username.invalid) {
      this.uiService.showMessage('Username should be at least 3 symbols');
      return;
    }

    if (this.password.invalid) {
      this.uiService.showMessage('Password should be at least 3 symbols');
      return;
    }

    if (this.repeatPassword.value !== this.password.value) {
      this.uiService.showMessage('Password and Repeat password values should match');
      return;
    }

    if (this.registerForm.invalid) {
      return;
    }

    this.loading = true;
    this.authService.register$(this.registerForm.value)
      .subscribe(
        () => this.uiService.navigate('/'),
        () => this.loading = false
      );
  }

  public onFormTap(): void {
    utils.ad.dismissSoftInput();
  }
}
