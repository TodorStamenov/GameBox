import { NgModule } from '@angular/core';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { EffectsModule } from '@ngrx/effects';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { environment } from 'src/environments/environment';
import { AppRoutingModule } from './app.routing';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { AppComponent } from './app.component';
import { CoreModule } from './modules/core/core.module';
import { GraphQLModule } from './graphql.module';
import { coreReducer } from './store/+store/core/core.reducer';
import { categoriesReducer } from './store/+store/category/categories.reducer';
import { CategoriesEffects } from './store/+store/category/categories.effects';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    AppRoutingModule,
    StoreModule.forRoot({
      core: coreReducer,
      categories: categoriesReducer
    }),
    EffectsModule.forRoot([
      CategoriesEffects
    ]),
    StoreDevtoolsModule.instrument({
      name: 'Game Box App Devtools',
      maxAge: 25,
      logOnly: environment.production
    , connectInZone: true}),
    GraphQLModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: JwtInterceptor,
    multi: true
  },
  {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
