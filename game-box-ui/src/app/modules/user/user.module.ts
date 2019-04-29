import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { userComponents } from '.';
import { UserRoutingModule } from './user-routing.module';
import { CoreModule } from '../core/core.module';

@NgModule({
  declarations: [
    ...userComponents
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    CoreModule
  ]
})
export class UserModule { }
