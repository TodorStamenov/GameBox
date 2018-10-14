import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryComponent } from './components/category/category.component';
import { GameComponent } from './components/game/game.component';
import { UserComponent } from './components/user/user.component';

const adminRoute: Routes = [
  { path: 'categories', component: CategoryComponent },
  { path: 'games', component: GameComponent },
  { path: 'users', component: UserComponent }
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