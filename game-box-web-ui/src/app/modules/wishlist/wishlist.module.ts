import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WishlistRoutingModule } from './wishlist-routing.module';
import { CoreModule } from '../core/core.module';
import { WishlistComponent } from './components/wishlist/wishlist.component';

@NgModule({
  declarations: [
    WishlistComponent
  ],
  imports: [
    CommonModule,
    CoreModule,
    WishlistRoutingModule
  ]
})
export class WishlistModule { }
