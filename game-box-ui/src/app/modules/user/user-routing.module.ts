import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { CategoryGamesComponent } from './components/game/category-games/category-games.component';
import { OwnedGamesComponent } from './components/game/owned-games/owned-games.component';
import { GameDetailsComponent } from './components/game/game-details/game-details.component';

const userRoute: Routes = [
  { path: '', component: HomeComponent },
  { path: 'games/owned', component: OwnedGamesComponent },
  { path: 'games/details/:id', component: GameDetailsComponent },
  { path: 'category/:categoryId/games', component: CategoryGamesComponent}
];

@NgModule({
  imports: [
    RouterModule.forChild(userRoute)
  ],
  exports: [
    RouterModule
  ]
})
export class UserRoutingModule { }