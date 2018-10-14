import { NgModule } from '@angular/core';
import { adminComponents } from '.'
import { AdminService } from './admin.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { CategoryComponent } from './components/category/category.component';
import { GameComponent } from './components/game/game.component';
import { AdminRoutingModule } from './admin-routing.module';

@NgModule({
  declarations: [
    ...adminComponents,
    CategoryComponent,
    GameComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    AdminRoutingModule
  ],
  providers: [
    AdminService
  ]
})
export class AdminModule { }
