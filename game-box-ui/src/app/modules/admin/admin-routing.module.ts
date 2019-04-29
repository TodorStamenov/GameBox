import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AllCategoriesComponent } from './components/category/all-categories/all-categories.component';
import { CategoryComponent } from './components/category/category/category.component';
import { GameComponent } from './components/game/game/game.component';
import { DeleteGameComponent } from './components/game/delete-game/delete-game.component';
import { AllUsersComponent } from './components/user/all-users/all-users.component';
import { AllOrdersComponent } from './components/order/all-orders/all-orders.component';
import { CreateUserComponent } from './components/user/create-user/create-user.component';

const adminRoute: Routes = [
  { path: 'categories/all', component: AllCategoriesComponent },
  { path: 'categories/:action', component: CategoryComponent },
  { path: 'games/:action', component: GameComponent },
  { path: 'games/delete/:id', component: DeleteGameComponent },
  { path: 'users/all', component: AllUsersComponent },
  { path: 'users/create', component: CreateUserComponent },
  { path: 'orders/all', component: AllOrdersComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(adminRoute)
  ],
  exports: [
    RouterModule
  ]
})
export class AdminRoutingModule { }
