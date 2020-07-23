import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';

import { CartRoutingModule } from './cart-routing.module';
import { CartComponent } from './components/cart/cart.component';
import { CoreModule } from '../core/core.module';
import { cartReducer } from './+store/cart.reducer';
import { CartEffects } from './+store/cart.effects';

@NgModule({
  declarations: [
    CartComponent
  ],
  imports: [
    CommonModule,
    CoreModule,
    CartRoutingModule,
    StoreModule.forFeature('cart', cartReducer),
    EffectsModule.forFeature([CartEffects])
  ]
})
export class CartModule { }
