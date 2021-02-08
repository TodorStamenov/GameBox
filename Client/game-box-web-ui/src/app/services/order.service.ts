import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

import { IOrdersListModel } from '../modules/order/models/orders-list.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private nodeOrdersUrl = environment.api.ordersApiUrl + 'orders';
  private coreOrdersUrl = environment.api.gamesApiUrl + 'orders';

  constructor(private http: HttpClient) { }

  public getOrders$(startDate: string, endDate: string): Observable<IOrdersListModel[]> {
    return this.http.get<IOrdersListModel[]>(this.nodeOrdersUrl + '?startDate=' + startDate + '&endDate=' + endDate).pipe(take(1));
  }

  public createOrder$(gameIds: string[]): Observable<void> {
    return this.http.post<void>(this.coreOrdersUrl, gameIds).pipe(take(1));
  }
}
