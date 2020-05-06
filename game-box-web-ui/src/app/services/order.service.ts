import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { IOrdersListModel } from '../modules/order/models/orders-list.model';
import { environment } from 'src/environments/environment';

const nodeOrdersUrl = environment.api.nodeUrl + 'orders';
const coreOrdersUrl = environment.api.baseUrl + 'orders';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  constructor(private http: HttpClient) { }

  public getOrders$(startDate: string, endDate: string): Observable<IOrdersListModel[]> {
    return this.http.get<IOrdersListModel[]>(nodeOrdersUrl + '?startDate=' + startDate + '&endDate=' + endDate);
  }

  public createOrder$(gameIds: string[]): Observable<void> {
    return this.http.post<void>(coreOrdersUrl, gameIds);
  }
}
