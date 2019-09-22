import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Store } from '@ngrx/store';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { constants } from 'src/app/common';
import { IOrdersListModel } from '../models/orders/orders-list.model';
import { IAppState } from 'src/app/store/app.state';
import { GetAllOrders } from 'src/app/store/orders/orders.actions';

const ordersUrl = constants.host + 'orders';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  constructor(
    private http: HttpClient,
    private store: Store<IAppState>
  ) { }

  public getOrders$(startDate: string, endDate: string): Observable<void> {
    return this.http.get<IOrdersListModel[]>(ordersUrl + '?startDate=' + startDate + '&endDate=' + endDate).pipe(
      map((orders: IOrdersListModel[]) => {
        this.store.dispatch(new GetAllOrders(orders));
      })
    );
  }
}
