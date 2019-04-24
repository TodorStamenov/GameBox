import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { authComponents } from '.';
import { AuthRoutingModule } from './auth-routing.module';
import { ChangePasswordComponent } from './components/change-password/change-password.component';
import { CoreModule } from '../core/core.module';

@NgModule({
  declarations: [
    ...authComponents,
    ChangePasswordComponent
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    AuthRoutingModule,
    CoreModule
  ]
})
export class AuthModule { }
