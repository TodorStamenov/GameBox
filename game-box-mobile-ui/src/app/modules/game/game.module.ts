import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { NativeScriptCommonModule } from 'nativescript-angular/common';
import { GameRoutingModule } from './game-routing.module';
import { GamesTabsComponent } from './components/game-tabs/game-tabs.component';
import { GameItemsComponent } from './components/game-items/game-items.component';
import { GameDetailsComponent } from './components/game-details/game-details.component';

@NgModule({
  declarations: [
    GamesTabsComponent,
    GameItemsComponent,
    GameDetailsComponent
  ],
  imports: [
    NativeScriptCommonModule,
    GameRoutingModule
  ],
  schemas: [NO_ERRORS_SCHEMA]
})
export class GameModule { }