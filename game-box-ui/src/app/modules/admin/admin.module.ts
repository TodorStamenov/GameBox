import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { adminComponents } from '.';
import { AdminService } from './services/admin.service';
import { AdminRoutingModule } from './admin-routing.module';
import { CategoryService } from './services/category.service';
import { OrderService } from './services/order.service';
import { GameService } from './services/game.service';
import { CoreModule } from '../core/core.module';

@NgModule({
  declarations: [
    ...adminComponents
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    AdminRoutingModule,
    CoreModule
  ],
  providers: [
    AdminService,
    CategoryService,
    GameService,
    OrderService
  ]
})
export class AdminModule { }