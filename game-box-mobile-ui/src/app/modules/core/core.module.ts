import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';

import { NativeScriptCommonModule } from 'nativescript-angular/common';
import { ActionBarComponent } from './components/action-bar.component';

@NgModule({
  declarations: [
    ActionBarComponent
  ],
  imports: [
    NativeScriptCommonModule
  ],
  exports: [
    ActionBarComponent
  ],
  schemas: [NO_ERRORS_SCHEMA]
})
export class CoreModule { }
