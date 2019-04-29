import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { adminComponents } from '.';
import { AdminRoutingModule } from './admin-routing.module';

@NgModule({
  declarations: [
    ...adminComponents
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    AdminRoutingModule
  ]
})
export class AdminModule { }
