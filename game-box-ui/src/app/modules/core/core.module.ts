import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { coreComponents } from '.';

@NgModule({
  declarations: [
    ...coreComponents
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    ...coreComponents
  ]
})
export class CoreModule { }
