import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, Validators, FormBuilder, AbstractControl } from '@angular/forms';

import { AuthService } from 'src/app/services/auth.service';
import { NotificationsService } from 'src/app/services/notifications.service';
import { FormService } from 'src/app/services/form.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  public loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private notificationService: NotificationsService,
    public formService: FormService
  ) { }

  public ngOnInit(): void {
    this.loginForm = this.fb.group({
      'username': [null, [Validators.required, Validators.minLength(3)]],
      'password': [null, [Validators.required, Validators.minLength(3)]]
    });
  }

  public getField(name: string): AbstractControl {
    return this.loginForm.get(name);
  }

  public login(): void {
    this.authService.login$(this.loginForm.value)
      .subscribe(() => {
        this.router.navigate(['/']);
        this.notificationService.subscribeForNotifications(this.authService.user.token);
      });
  }
}
