import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';

import { mergeMap, map } from 'rxjs/operators';

import { WishlistService } from 'src/app/modules/wishlist/services/wishlist.service';
import { WishlistActionTypes, GetAllItems } from './wishlist.actions';

@Injectable()
export class WishlistEffects {
  constructor(
    private actions$: Actions,
    private wishlistService: WishlistService
  ) { }

  @Effect()
  loadCategories$ = this.actions$.pipe(
    ofType(WishlistActionTypes.LoadAllItems),
    mergeMap(() => this.wishlistService.getItems$().pipe(
      map(items => new GetAllItems(items))
    ))
  );
}
