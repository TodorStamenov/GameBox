import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { userComponents } from '.';
import { UserRoutingModule } from './user-routing.module';
import { CoreModule } from '../core/core.module';
import { GameService } from './services/game.service';
import { CartService } from './services/cart.service';
import { OrderService } from './services/order.service';
import { LoadMoreComponent } from './components/shared/load-more/load-more.component';

@NgModule({
  declarations: [...userComponents, LoadMoreComponent],
  imports: [
    CommonModule,
    UserRoutingModule,
    CoreModule
  ],
  providers: [
    GameService,
    CartService,
    OrderService
  ]
})
export class UserModule { }
