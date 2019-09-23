import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AdminGuard } from './guards/admin.guard';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/games' },
  { path: 'auth', loadChildren: () => import('./modules/auth/auth.module').then(m => m.AuthModule) },
  { path: 'cart', loadChildren: () => import('./modules/cart/cart.module').then(m => m.CartModule) },
  { path: 'games', loadChildren: () => import('./modules/game/game.module').then(m => m.GameModule) },
  {
    path: 'categories',
    loadChildren: () => import('./modules/category/category.module').then(m => m.CategoryModule),
    canActivate: [AdminGuard]
  },
  {
    path: 'orders',
    loadChildren: () => import('./modules/order/order.module').then(m => m.OrderModule),
    canActivate: [AdminGuard]
  },
  {
    path: 'users',
    loadChildren: () => import('./modules/user/user.module').then(m => m.UserModule),
    canActivate: [AdminGuard]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
