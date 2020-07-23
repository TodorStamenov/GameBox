import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';

import { mergeMap, map } from 'rxjs/operators';

import { WishlistService } from 'src/app/services/wishlist.service';
import { WishlistActionTypes, GetAllItems, RemoveItem, UnloadItem, ClearItems } from './wishlist.actions';

@Injectable()
export class WishlistEffects {
  constructor(
    private actions$: Actions,
    private wishlistService: WishlistService
  ) { }

  @Effect()
  loadWishlistItems$ = this.actions$.pipe(
    ofType(WishlistActionTypes.LoadAllItems),
    mergeMap(() => this.wishlistService.getItems$().pipe(
      map(items => new GetAllItems(items))
    ))
  );

  @Effect()
  removeWishlistItem$ = this.actions$.pipe(
    ofType(WishlistActionTypes.UnloadItem),
    mergeMap((action: UnloadItem) => this.wishlistService.removeItem$(action.payload).pipe(
      map(() => new RemoveItem(action.payload))
    ))
  );

  @Effect()
  clearWishlistItems$ = this.actions$.pipe(
    ofType(WishlistActionTypes.UnloadItems),
    mergeMap(() => this.wishlistService.clearItems$().pipe(
      map(() => new ClearItems())
    ))
  );
}
