import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { constants } from '../../../common';

const ordersUrl = constants.host + 'orders/'

@Injectable()
export class OrderService {
  constructor(private http: HttpClient) { }

  createOrder(gameIds: string[]) {
    return this.http.post(ordersUrl, gameIds);
  }
}