import { NgModule } from '@angular/core';
import { adminComponents } from '.'
import { AdminService } from './services/admin.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { AdminRoutingModule } from './admin-routing.module';
import { CategoryService } from './services/category.service';
import { OrderService } from './services/order.service';
import { GameService } from './services/game.service';

@NgModule({
  declarations: [
    ...adminComponents
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    AdminRoutingModule,
  ],
  providers: [
    AdminService,
    CategoryService,
    GameService,
    OrderService
  ]
})
export class AdminModule { }
