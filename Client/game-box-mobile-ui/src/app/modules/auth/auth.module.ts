import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { NativeScriptCommonModule } from 'nativescript-angular/common';
import { NativeScriptFormsModule } from 'nativescript-angular/forms';
import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { CoreModule } from '../core/core.module';
import { ChangePasswordComponent } from './components/change-password/change-password.component';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    ChangePasswordComponent
  ],
  imports: [
    CoreModule,
    AuthRoutingModule,
    NativeScriptCommonModule,
    NativeScriptFormsModule,
    ReactiveFormsModule
  ],
  schemas: [NO_ERRORS_SCHEMA]
})
export class AuthModule { }
