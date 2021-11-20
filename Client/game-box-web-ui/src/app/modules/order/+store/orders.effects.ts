import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';

import { mergeMap, map } from 'rxjs/operators';

import { OrderService } from 'src/app/services/order.service';
import { OrderActionTypes, LoadAllOrders, GetAllOrders } from './orders.actions';

@Injectable()
export class OrdersEffects {
  constructor(
    private actions$: Actions,
    private orderService: OrderService
  ) { }

  loadOrders$ = createEffect(() => this.actions$.pipe(
    ofType(OrderActionTypes.LoadAllOrders),
    mergeMap((action: LoadAllOrders) => this.orderService.getOrders$(action.payload.startDate, action.payload.endDate).pipe(
      map(orders => new GetAllOrders(orders))
    ))
  ));
}
