import { NgModule } from '@angular/core';
import { Routes, PreloadAllModules } from '@angular/router';

import { NativeScriptRouterModule } from 'nativescript-angular/router';

const routes: Routes = [

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
