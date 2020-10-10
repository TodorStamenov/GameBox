import { NgModule } from '@angular/core';
import { Routes } from '@angular/router';
import { NativeScriptRouterModule } from '@nativescript/angular';

import { WishlistComponent } from './components/wishlist.component';

const routes: Routes = [
  { path: 'items', component: WishlistComponent }
 ];

 @NgModule({
   imports: [NativeScriptRouterModule.forChild(routes)],
   exports: [NativeScriptRouterModule]
 })
export class WishlistRoutingModule { }
