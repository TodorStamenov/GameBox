import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { constants } from 'src/app/common';
import { IOrderModel } from '../models/orders/order.model';

const ordersUrl = constants.host + 'orders';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  constructor(private http: HttpClient) { }

  public getOrders$(startDate: string, endDate: string): Observable<IOrderModel[]> {
    return this.http.get<IOrderModel[]>(ordersUrl + '?startDate=' + startDate + '&endDate=' + endDate);
  }
}
