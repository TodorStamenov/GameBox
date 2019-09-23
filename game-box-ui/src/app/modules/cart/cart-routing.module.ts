import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ItemsListComponent } from '../cart/components/items-list/items-list.component';

const cartRoutes: Routes = [
  { path: 'items', component: ItemsListComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(cartRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class CartRoutingModule { }
