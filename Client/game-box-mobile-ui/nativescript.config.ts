import { NativeScriptConfig } from '@nativescript/core';

export default {
  id: 'org.nativescript.gameboxmobileui',
  appResourcesPath: 'App_Resources',
  appPath: 'src',
  android: {
    v8Flags: '--expose_gc',
    markingMode: 'none'
  }
} as NativeScriptConfig;
