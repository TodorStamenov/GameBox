import { NgModule } from '@angular/core';
import { Routes } from '@angular/router';

import { WishlistComponent } from './components/wishlist.component';
import { NativeScriptRouterModule } from 'nativescript-angular/router';

const routes: Routes = [
  { path: 'items', component: WishlistComponent }
 ];

 @NgModule({
   imports: [NativeScriptRouterModule.forChild(routes)],
   exports: [NativeScriptRouterModule]
 })
export class WishlistRoutingModule { }
