import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { NativeScriptCommonModule, NativeScriptFormsModule } from '@nativescript/angular';
import { NativeScriptMaterialTabsModule } from '@nativescript-community/ui-material-tabs/angular';

import { GameRoutingModule } from './game-routing.module';
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
    NativeScriptMaterialTabsModule,
    NativeScriptCommonModule,
    NativeScriptFormsModule,
    ReactiveFormsModule,
    GameRoutingModule,
    CoreModule,
  ],
  schemas: [NO_ERRORS_SCHEMA]
})
export class GameModule { }
