import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { NavigationComponent } from './components/navigation/navigation.component';
import { FooterComponent } from './components/footer/footer.component';
import { SliceStringPipe } from './pipes/sliceString.pipe';
import { GameListItemsComponent } from './components/game-list-items/game-list-items.component';
import { SearchFormComponent } from './components/search-form/search-form.component';

const components = [
  NavigationComponent,
  FooterComponent,
  SearchFormComponent,
  GameListItemsComponent,
  SliceStringPipe
];

@NgModule({
  declarations: [components],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [components]
})
export class CoreModule { }
