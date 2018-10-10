import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Components
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './authentication/login/login.component';
import { RegisterComponent } from './authentication/register/register.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'user/login', component: LoginComponent },
  { path: 'user/register', component: RegisterComponent }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }