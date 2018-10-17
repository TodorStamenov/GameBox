import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../../services/order.service';
import { OrderModel } from '../../../models/orders/order.model';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';

@Component({
  selector: 'app-all-orders',
  templateUrl: './all-orders.component.html'
})
export class AllOrdersComponent implements OnInit {
  public dateRange: FormGroup;

  public orders: OrderModel[];

  constructor(
    private fb: FormBuilder,
    private orderService: OrderService
  ) { }

  ngOnInit() {
    this.getOrders('', '');

    this.dateRange = this.fb.group({
      startDate: new FormControl(''),
      endDate: new FormControl('')
    });
  }

  getOrders(startDate: string, endDate: string) {
    this.orderService
      .getOrders(startDate, endDate)
      .subscribe(res => this.orders = res);
  }

  filterOrders() {
    this.getOrders(
      this.dateRange.controls.startDate.value, 
      this.dateRange.controls.endDate.value);
  }
}