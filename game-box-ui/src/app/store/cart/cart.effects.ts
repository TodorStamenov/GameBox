import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';

import { mergeMap, map } from 'rxjs/operators';

import { CartService } from 'src/app/modules/cart/services/cart.service';
import { CartActionTypes, GetAllItems } from './cart.actions';

@Injectable()
export class CartEffects {
  constructor(
    private actions$: Actions,
    private cartService: CartService
  ) { }

  @Effect()
  loadCategories$ = this.actions$.pipe(
    ofType(CartActionTypes.LoadAllItems),
    mergeMap(() => this.cartService.getCart$().pipe(
      map(items => new GetAllItems(items))
    ))
  );
}
