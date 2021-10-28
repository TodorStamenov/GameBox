import { runNativeScriptAngularApp, platformNativeScript } from '@nativescript/angular';
import 'zone.js';

import { AppModule } from './app/app.module';

runNativeScriptAngularApp({
  appModuleBootstrap: () => platformNativeScript().bootstrapModule(AppModule)
});
