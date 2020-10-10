import { NgModule } from '@angular/core';
import { Routes, PreloadAllModules } from '@angular/router';
import { NativeScriptRouterModule } from '@nativescript/angular';

import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/games' },
  {
    path: 'games',
    loadChildren: () => import('~/app/modules/game/game.module').then(m => m.GameModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'cart',
    loadChildren: () => import('~/app/modules/cart/cart.module').then(m => m.CartModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'wishlist',
    loadChildren: () => import('~/app/modules/wishlist/wishlist.module').then(m => m.WishlistModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'auth',
    loadChildren: () => import('~/app/modules/auth/auth.module').then(m => m.AuthModule)
  }
];

@NgModule({
  imports: [
    NativeScriptRouterModule.forRoot(routes, {
      preloadingStrategy: PreloadAllModules
    })
  ],
  exports: [NativeScriptRouterModule]
})
export class AppRoutingModule { }
