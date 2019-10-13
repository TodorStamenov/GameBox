import { NgModule } from '@angular/core';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { EffectsModule } from '@ngrx/effects';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ToastrModule } from 'ngx-toastr';

import { environment } from 'src/environments/environment';
import { AppRoutingModule } from './app.routing';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { AppComponent } from './app.component';
import { CoreModule } from './modules/core/core.module';
import { appReducers } from './store/app.reducers';
import { CategoriesEffects } from './store/categories/categories.effects';
import { OrdersEffects } from './store/orders/orders.effects';
import { UsersEffects } from './store/users/users.effects';
import { GamesEffects } from './store/games/games.effects';
import { CartEffects } from './store/cart/cart.effects';

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
    ToastrModule.forRoot(),
    StoreModule.forRoot(appReducers),
    EffectsModule.forRoot([
      CategoriesEffects,
      CartEffects,
      OrdersEffects,
      UsersEffects,
      GamesEffects
    ]),
    StoreDevtoolsModule.instrument({
      name: 'Game Box App Devtools',
      maxAge: 25,
      logOnly: environment.production
    })
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
