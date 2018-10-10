import { NgModule } from '@angular/core';
import { authComponents } from '.';
import { FormsModule } from '@angular/forms';
import { AuthService } from './auth.service';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

@NgModule({
  declarations: [
    ...authComponents,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    FormsModule
  ],
  providers: [
    AuthService
  ]
})
export class AuthModule { }