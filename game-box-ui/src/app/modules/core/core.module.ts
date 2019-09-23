import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { NavigationComponent } from './components/navigation/navigation.component';
import { FooterComponent } from './components/footer/footer.component';
import { SliceStringPipe } from './pipes/sliceString.pipe';
import { SearchFormComponent } from './components/search-form/search-form.component';

@NgModule({
  declarations: [
    NavigationComponent,
    FooterComponent,
    SearchFormComponent,
    SliceStringPipe
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    NavigationComponent,
    FooterComponent,
    SearchFormComponent,
    SliceStringPipe
  ]
})
export class CoreModule { }
