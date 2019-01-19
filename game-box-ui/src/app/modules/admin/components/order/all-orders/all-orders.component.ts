import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';

import { Observable } from 'rxjs';

import { OrderService } from '../../../services/order.service';
import { OrderModel } from '../../../models/orders/order.model';

@Component({
  selector: 'app-all-orders',
  templateUrl: './all-orders.component.html'
})
export class AllOrdersComponent implements OnInit {
  public orders$: Observable<OrderModel[]>;
  public dateRange: FormGroup;

  constructor(
    private fb: FormBuilder,
    private orderService: OrderService
  ) { }

  public ngOnInit(): void {
    this.dateRange = this.fb.group({
      'startDate': new FormControl(''),
      'endDate': new FormControl('')
    });

    this.getOrders('', '');
  }

  public getOrders(startDate: string, endDate: string): void {
    this.orders$ = this.orderService.getOrders(startDate, endDate);
  }

  public filterOrders(): void {
    this.getOrders(
      this.dateRange.controls.startDate.value,
      this.dateRange.controls.endDate.value);
  }
}
