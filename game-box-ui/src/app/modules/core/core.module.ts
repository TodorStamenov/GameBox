import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { FooterComponent } from './components/footer/footer.component';
import { NavigationComponent } from './components/navigation/navigation.component';
import { SliceStringPipe } from './pipes/sliceString.pipe';

@NgModule({
  declarations: [
    NavigationComponent,
    FooterComponent,
    SliceStringPipe
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    NavigationComponent,
    FooterComponent,
    SliceStringPipe
  ]
})
export class CoreModule { }
