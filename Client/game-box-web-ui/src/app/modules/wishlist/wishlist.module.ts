import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';

import { WishlistRoutingModule } from './wishlist-routing.module';
import { CoreModule } from '../core/core.module';
import { WishlistComponent } from './components/wishlist/wishlist.component';
import { wishlistReducer } from './+store/wishlist.reducer';
import { WishlistEffects } from './+store/wishlist.effects';

@NgModule({
  declarations: [
    WishlistComponent
  ],
  imports: [
    CommonModule,
    CoreModule,
    WishlistRoutingModule,
    StoreModule.forFeature('wishlist', wishlistReducer),
    EffectsModule.forFeature([WishlistEffects])
  ]
})
export class WishlistModule { }
