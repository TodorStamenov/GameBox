import { Injectable } from "@angular/core";
import { constants } from "src/app/common";
import { HttpClient } from "@angular/common/http";
import { OrderModel } from "../models/orders/order.model";

const ordersUrl = constants.host + 'orders';


@Injectable()
export class OrderService {
  constructor(private http: HttpClient) { }

  getOrders(startDate: string, endDate: string) {
    return this.http.get<OrderModel[]>(ordersUrl + '?startDate=' + startDate + '&endDate=' + endDate);
  }
}