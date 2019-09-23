import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { UserRoutingModule } from './user-routing.module';
import { CoreModule } from '../core/core.module';
import { CreateUserComponent } from './components/create-user/create-user.component';
import { UsersListComponent } from './components/users-list/users-list.component';

@NgModule({
  declarations: [
    CreateUserComponent,
    UsersListComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    ReactiveFormsModule,
    CoreModule
  ]
})
export class UserModule { }
