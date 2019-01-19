import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AllCategoriesComponent } from './components/category/all-categories/all-categories.component';
import { CreateCategoryComponent } from './components/category/create-category/create-category.component';
import { EditCategoryComponent } from './components/category/edit-category/edit-category.component';
import { CreateGameComponent } from './components/game/create-game/create-game.component';
import { EditGameComponent } from './components/game/edit-game/edit-game.component';
import { DeleteGameComponent } from './components/game/delete-game/delete-game.component';
import { AllUsersComponent } from './components/user/all-users/all-users.component';
import { AllOrdersComponent } from './components/order/all-orders/all-orders.component';

const adminRoute: Routes = [
  { path: 'categories/all', component: AllCategoriesComponent },
  { path: 'categories/create', component: CreateCategoryComponent },
  { path: 'categories/edit/:id', component: EditCategoryComponent },
  { path: 'games/create', component: CreateGameComponent },
  { path: 'games/edit/:id', component: EditGameComponent },
  { path: 'games/delete/:id', component: DeleteGameComponent },
  { path: 'users/all', component: AllUsersComponent },
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
