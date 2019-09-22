import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { constants } from '../../../common';

const ordersUrl = constants.host + 'orders/';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  constructor(private http: HttpClient) { }

  public createOrder$(gameIds: string[]): Observable<void> {
    return this.http.post<void>(ordersUrl, gameIds);
  }
}
