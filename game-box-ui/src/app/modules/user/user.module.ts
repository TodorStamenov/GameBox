import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { userComponents } from '.';
import { UserRoutingModule } from './user-routing.module';
import { GameService } from './services/game.service';

@NgModule({
  declarations: [...userComponents],
  imports: [
    CommonModule,
    UserRoutingModule
  ],
  providers: [
    GameService
  ]
})
export class UserModule { }