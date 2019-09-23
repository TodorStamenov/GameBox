import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { OrdersListComponent } from '../order/components/orders-list/orders-list.component';

const orderRoute: Routes = [
  { path: 'all', component: OrdersListComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(orderRoute)
  ],
  exports: [
    RouterModule
  ]
})
export class OrderRoutingModule { }
