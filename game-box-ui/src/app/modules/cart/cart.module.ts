import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CartRoutingModule } from './cart-routing.module';
import { ItemsListComponent } from './components/items-list/items-list.component';
import { CoreModule } from '../core/core.module';

@NgModule({
  declarations: [
    ItemsListComponent
  ],
  imports: [
    CommonModule,
    CoreModule,
    CartRoutingModule
  ]
})
export class CartModule { }
