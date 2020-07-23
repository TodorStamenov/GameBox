import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { WishlistComponent } from './components/wishlist.component';
import { CoreModule } from '../core/core.module';
import { WishlistRoutingModule } from './wishlist-routing.module';
import { NativeScriptCommonModule } from 'nativescript-angular/common';

@NgModule({
  declarations: [
    WishlistComponent
  ],
  imports: [
    NativeScriptCommonModule,
    CoreModule,
    WishlistRoutingModule
  ],
  schemas: [NO_ERRORS_SCHEMA]
})
export class WishlistModule { }
