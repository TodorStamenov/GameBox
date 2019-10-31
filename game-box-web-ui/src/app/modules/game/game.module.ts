import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { GameRoutingModule } from './game-routing.module';
import { GamesListComponent } from './components/games-list/games-list.component';
import { GameComponent } from './components/game/game.component';
import { DeleteGameComponent } from './components/delete-game/delete-game.component';
import { GameDetailsComponent } from './components/game-details/game-details.component';
import { HomeComponent } from './components/home/home.component';
import { OwnedGamesComponent } from './components/owned-games/owned-games.component';
import { GameCardListComponent } from './components/shared/game-card-list/game-card-list.component';
import { LoadMoreComponent } from './components/shared/load-more/load-more.component';
import { CategoryGamesComponent } from './components/category-games/category-games.component';
import { CoreModule } from '../core/core.module';

@NgModule({
  declarations: [
    GamesListComponent,
    GameComponent,
    DeleteGameComponent,
    GameDetailsComponent,
    HomeComponent,
    OwnedGamesComponent,
    GameCardListComponent,
    LoadMoreComponent,
    CategoryGamesComponent
  ],
  imports: [
    CommonModule,
    CoreModule,
    ReactiveFormsModule,
    GameRoutingModule
  ]
})
export class GameModule { }
