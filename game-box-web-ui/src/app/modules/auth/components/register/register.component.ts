import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';

import { AuthService } from 'src/app/services/auth.service';
import { NotificationsService } from 'src/app/services/notifications.service';
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
    private notificationService: NotificationsService,
    public formService: FormService
  ) { }

  public ngOnInit(): void {
    this.registerForm = this.fb.group({
      'username': [null, [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      'password': [null, [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      'repeatPassword': [null]
    });
  }

  public getField(name: string): AbstractControl {
    return this.registerForm.get(name);
  }

  public register(): void {
    this.authService.register$(this.registerForm.value)
      .subscribe(() => {
        this.router.navigate(['/']);
        this.notificationService.subscribeForNotifications(this.authService.user.token);
      });
  }
}
