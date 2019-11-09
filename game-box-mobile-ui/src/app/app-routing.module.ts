import { NgModule } from '@angular/core';
import { Routes, PreloadAllModules } from '@angular/router';

import { NativeScriptRouterModule } from 'nativescript-angular/router';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/games' },
  { path: 'games', loadChildren: '~/app/modules/game/game.module#GameModule', canActivate: [AuthGuard] },
  { path: 'auth', loadChildren: '~/app/modules/auth/auth.module#AuthModule' }
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
