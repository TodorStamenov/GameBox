import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CategoriesListComponent } from './components/category/categories-list/categories-list.component';
import { CategoryComponent } from './components/category/category/category.component';
import { GameComponent } from './components/game/game/game.component';
import { DeleteGameComponent } from './components/game/delete-game/delete-game.component';
import { UsersListComponent } from './components/user/users-list/users-list.component';
import { OrdersListComponent } from './components/order/orders-list/orders-list.component';
import { CreateUserComponent } from './components/user/create-user/create-user.component';
import { GamesListComponent } from './components/game/games-list/games-list.component';

const adminRoute: Routes = [
  { path: 'categories/all', component: CategoriesListComponent },
  { path: 'categories/:action', component: CategoryComponent },
  { path: 'games/all', component: GamesListComponent },
  { path: 'games/:action', component: GameComponent },
  { path: 'games/delete/:id', component: DeleteGameComponent },
  { path: 'users/all', component: UsersListComponent },
  { path: 'users/create', component: CreateUserComponent },
  { path: 'orders/all', component: OrdersListComponent }
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
