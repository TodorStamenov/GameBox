import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';

import { mergeMap, map } from 'rxjs/operators';

import { CartService } from 'src/app/services/cart.service';
import { CartActionTypes, GetAllItems, UnloadItem, RemoveItem, ClearItems } from './cart.actions';

@Injectable()
export class CartEffects {
  constructor(
    private actions$: Actions,
    private cartService: CartService
  ) { }

  loadCartItems$ = createEffect(() => this.actions$.pipe(
    ofType(CartActionTypes.LoadAllItems),
    mergeMap(() => this.cartService.getCart$().pipe(
      map(items => new GetAllItems(items))
    ))
  ));

  removeCartItem$ = createEffect(() => this.actions$.pipe(
    ofType(CartActionTypes.UnloadItem),
    mergeMap((action: UnloadItem) => this.cartService.removeItem$(action.payload).pipe(
      map(id => new RemoveItem(id))
    ))
  ));

  clearCartItems$ = createEffect(() => this.actions$.pipe(
    ofType(CartActionTypes.UnloadItems),
    mergeMap(() => this.cartService.clear$().pipe(
      map(() => new ClearItems())
    ))
  ));
}
