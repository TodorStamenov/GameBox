import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { constants } from 'src/app/common';
import { OrderModel } from '../models/orders/order.model';

const ordersUrl = constants.host + 'orders';

@Injectable()
export class OrderService {
  constructor(private http: HttpClient) { }

  public getOrders(startDate: string, endDate: string): Observable<OrderModel[]> {
    return this.http.get<OrderModel[]>(ordersUrl + '?startDate=' + startDate + '&endDate=' + endDate);
  }
}
