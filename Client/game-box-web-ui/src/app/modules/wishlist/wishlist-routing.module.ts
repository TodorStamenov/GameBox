import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { WishlistComponent } from './components/wishlist/wishlist.component';

const wishlistRoutes: Routes = [
  { path: 'items', component: WishlistComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(wishlistRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class WishlistRoutingModule { }
