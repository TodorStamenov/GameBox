import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AdminGuard } from './guards/admin.guard';

const routes: Routes = [
  { path: 'auth', loadChildren: './modules/authentication/auth.module#AuthModule' },
  { path: 'admin', loadChildren: './modules/admin/admin.module#AdminModule', canActivate: [AdminGuard] },
  { path: '', loadChildren: './modules/user/user.module#UserModule' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
