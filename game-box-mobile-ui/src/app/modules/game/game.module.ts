import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { NativeScriptCommonModule } from 'nativescript-angular/common';
import { GameRoutingModule } from './game-routing.module';
import { NativeScriptFormsModule } from 'nativescript-angular/forms';
import { GamesTabsComponent } from './components/game-tabs/game-tabs.component';
import { GameItemsComponent } from './components/game-items/game-items.component';
import { GameDetailsComponent } from './components/game-details/game-details.component';
import { GameCommentsComponent } from './components/game-comments/game-comments.component';
import { CoreModule } from '../core/core.module';

@NgModule({
  declarations: [
    GamesTabsComponent,
    GameItemsComponent,
    GameDetailsComponent,
    GameCommentsComponent
  ],
  imports: [
    NativeScriptCommonModule,
    NativeScriptFormsModule,
    ReactiveFormsModule,
    GameRoutingModule,
    CoreModule,
  ],
  schemas: [NO_ERRORS_SCHEMA]
})
export class GameModule { }
