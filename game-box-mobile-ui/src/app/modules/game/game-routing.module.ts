import { NgModule } from '@angular/core';
import { Routes } from '@angular/router';

import { NativeScriptRouterModule } from 'nativescript-angular/router';
import { GamesTabsComponent } from './components/game-tabs/game-tabs.component';
import { GameDetailsComponent } from './components/game-details/game-details.component';

const routes: Routes = [
 { path: '', pathMatch: 'full', component: GamesTabsComponent },
 { path: 'details/:id', component: GameDetailsComponent }
];

@NgModule({
  imports: [NativeScriptRouterModule.forChild(routes)],
  exports: [NativeScriptRouterModule]
})
export class GameRoutingModule { }
