import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';

import { OrderRoutingModule } from './order-routing.module';
import { OrdersListComponent } from './components/orders-list/orders-list.component';
import { ordersReducer } from './+store/orders.reducer';
import { OrdersEffects } from './+store/orders.effects';

@NgModule({
  declarations: [
    OrdersListComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    OrderRoutingModule,
    StoreModule.forFeature('orders', ordersReducer),
    EffectsModule.forFeature([OrdersEffects])
  ]
})
export class OrderModule { }
