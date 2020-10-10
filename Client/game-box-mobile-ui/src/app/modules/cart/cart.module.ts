import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { NativeScriptCommonModule } from '@nativescript/angular';

import { CartRoutingModule } from './cart-routing.module';
import { CoreModule } from '../core/core.module';
import { CartComponent } from './components/cart/cart.component';

@NgModule({
  declarations: [
    CartComponent
  ],
  imports: [
    NativeScriptCommonModule,
    CoreModule,
    CartRoutingModule
  ],
  schemas: [NO_ERRORS_SCHEMA]
})
export class CartModule { }
