import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Components
import { AdminGuard } from './guards/admin.guard';
import { AdminModule } from './modules/admin/admin.module'
import { AuthModule } from './modules/authentication/auth.module'
import { UserModule } from './modules/user/user.module';

const routes: Routes = [
  { path: '', pathMatch: 'full', loadChildren: () => UserModule },
  { path: 'auth', loadChildren: () => AuthModule },
  { path: 'admin', loadChildren: () => AdminModule, canActivate: [AdminGuard] }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }