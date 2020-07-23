import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { UsersListComponent } from './components/users-list/users-list.component';
import { CreateUserComponent } from './components/create-user/create-user.component';

const userRoutes: Routes = [
  { path: 'all', component: UsersListComponent },
  { path: 'create', component: CreateUserComponent },
];

@NgModule({
  imports: [
    RouterModule.forChild(userRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class UserRoutingModule { }
