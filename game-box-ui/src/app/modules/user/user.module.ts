import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { userComponents } from '.';
import { UserRoutingModule } from './user-routing.module';
import { GameService } from './services/game.service';
import { CartService } from './services/cart.service';
import { OrderService } from './services/order.service';

@NgModule({
  declarations: [...userComponents],
  imports: [
    CommonModule,
    UserRoutingModule
  ],
  providers: [
    GameService,
    CartService,
    OrderService
  ]
})
export class UserModule { }