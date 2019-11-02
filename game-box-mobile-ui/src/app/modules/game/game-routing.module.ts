import { NgModule } from '@angular/core';
import { Routes } from '@angular/router';

import { NativeScriptRouterModule } from 'nativescript-angular/router';
import { GamesListComponent } from './components/games-list/games-list.component';

const routes: Routes = [
 { path: '', pathMatch: 'full', component: GamesListComponent }
];

@NgModule({
  imports: [NativeScriptRouterModule.forChild(routes)],
  exports: [NativeScriptRouterModule]
})
export class GameRoutingModule { }
