import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { NativeScriptCommonModule } from 'nativescript-angular/common';
import { GameRoutingModule } from './game-routing.module';
import { GamesListComponent } from './components/games-list/games-list.component';

@NgModule({
  declarations: [
    GamesListComponent
  ],
  imports: [
    NativeScriptCommonModule,
    GameRoutingModule
  ],
  schemas: [NO_ERRORS_SCHEMA]
})
export class GameModule { }
