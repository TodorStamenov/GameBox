import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { constants } from 'src/app/common';
import { IOrdersListModel } from '../models/orders-list.model';

const nodeOrdersUrl = constants.nodeHost + 'orders';
const coreOrdersUrl = constants.apiHost + 'orders';

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
