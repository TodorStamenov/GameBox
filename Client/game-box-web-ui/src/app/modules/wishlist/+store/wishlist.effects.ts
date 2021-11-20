import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';

import { mergeMap, map } from 'rxjs/operators';

import { WishlistService } from 'src/app/services/wishlist.service';
import { WishlistActionTypes, GetAllItems, RemoveItem, UnloadItem, ClearItems } from './wishlist.actions';

@Injectable()
export class WishlistEffects {
  constructor(
    private actions$: Actions,
    private wishlistService: WishlistService
  ) { }

  loadWishlistItems$ = createEffect(() => this.actions$.pipe(
    ofType(WishlistActionTypes.LoadAllItems),
    mergeMap(() => this.wishlistService.getItems$().pipe(
      map(items => new GetAllItems(items))
    ))
  ));

  removeWishlistItem$ = createEffect(() => this.actions$.pipe(
    ofType(WishlistActionTypes.UnloadItem),
    mergeMap((action: UnloadItem) => this.wishlistService.removeItem$(action.payload).pipe(
      map(() => new RemoveItem(action.payload))
    ))
  ));

  clearWishlistItems$ = createEffect(() => this.actions$.pipe(
    ofType(WishlistActionTypes.UnloadItems),
    mergeMap(() => this.wishlistService.clearItems$().pipe(
      map(() => new ClearItems())
    ))
  ));
}
