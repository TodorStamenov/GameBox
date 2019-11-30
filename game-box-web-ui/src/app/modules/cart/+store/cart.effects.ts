import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';

import { mergeMap, map } from 'rxjs/operators';

import { CartService } from 'src/app/modules/cart/services/cart.service';
import { CartActionTypes, GetAllItems, UnloadItem, RemoveItem, ClearItems } from './cart.actions';

@Injectable()
export class CartEffects {
  constructor(
    private actions$: Actions,
    private cartService: CartService
  ) { }

  @Effect()
  loadCartItems$ = this.actions$.pipe(
    ofType(CartActionTypes.LoadAllItems),
    mergeMap(() => this.cartService.getCart$().pipe(
      map(items => new GetAllItems(items))
    ))
  );

  @Effect()
  removeCartItem$ = this.actions$.pipe(
    ofType(CartActionTypes.UnloadItem),
    mergeMap((action: UnloadItem) => this.cartService.removeItem$(action.payload).pipe(
      map(id => new RemoveItem(id))
    ))
  );

  @Effect()
  clearCartItems$ = this.actions$.pipe(
    ofType(CartActionTypes.UnloadItems),
    mergeMap(() => this.cartService.clear$().pipe(
      map(() => new ClearItems())
    ))
  );
}
