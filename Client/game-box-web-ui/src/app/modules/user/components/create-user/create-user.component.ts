import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, Validators, UntypedFormGroup, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';

import { UserService } from '../../../../services/user.service';
import { FormService } from 'src/app/services/form.service';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html'
})
export class CreateUserComponent implements OnInit {
  public createUserForm: UntypedFormGroup;

  constructor(
    private fb: UntypedFormBuilder,
    private userService: UserService,
    private router: Router,
    public formService: FormService
  ) { }

  public ngOnInit(): void {
    this.createUserForm = this.fb.group({
      'username': [null, [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      'password': [null, [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      'repeatPassword': [null]
    });
  }

  public getField(name: string): AbstractControl {
    return this.createUserForm.get(name);
  }

  public createUser(): void {
    this.userService.createUser$(this.createUserForm.value)
      .subscribe(() => this.router.navigate(['/users/all']));
  }
}
