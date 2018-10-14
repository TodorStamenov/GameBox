import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Components
import { HomeComponent } from './components/home/home.component';
import { AdminGuard } from './guards/admin.guard';
import { AdminModule } from './modules/admin/admin.module'
import { AuthModule } from './modules/authentication/auth.module'

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'auth', loadChildren: () => AuthModule },
  { path: 'admin', loadChildren: () => AdminModule, canActivate: [AdminGuard] }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }