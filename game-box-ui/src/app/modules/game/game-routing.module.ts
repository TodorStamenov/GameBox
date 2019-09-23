import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { GameComponent } from '../game/components/game/game.component';
import { DeleteGameComponent } from '../game/components/delete-game/delete-game.component';
import { GamesListComponent } from '../game/components/games-list/games-list.component';
import { HomeComponent } from './components/home/home.component';
import { OwnedGamesComponent } from './components/owned-games/owned-games.component';
import { GameDetailsComponent } from './components/game-details/game-details.component';
import { CategoryGamesComponent } from './components/category-games/category-games.component';
import { AdminGuard } from 'src/app/guards/admin.guard';
import { AuthGuard } from 'src/app/guards/auth.guard';

const gameRoute: Routes = [
  { path: '', component: HomeComponent },
  { path: 'owned', component: OwnedGamesComponent, canActivate: [AuthGuard] },
  { path: 'details/:id', component: GameDetailsComponent },
  { path: ':categoryId/games', component: CategoryGamesComponent },
  { path: 'all', component: GamesListComponent, canActivate: [AdminGuard] },
  { path: ':action', component: GameComponent, canActivate: [AdminGuard] },
  { path: 'delete/:id', component: DeleteGameComponent, canActivate: [AdminGuard] }
];

@NgModule({
  imports: [
    RouterModule.forChild(gameRoute)
  ],
  exports: [
    RouterModule
  ]
})
export class GameRoutingModule { }
