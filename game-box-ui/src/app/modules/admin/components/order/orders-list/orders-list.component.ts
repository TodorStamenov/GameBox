import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';

import { OrderService } from '../../../services/order.service';
import { IOrdersListModel } from '../../../models/orders/orders-list.model';
import { IAppState } from 'src/app/store/app.state';

@Component({
  selector: 'app-orders-list',
  templateUrl: './orders-list.component.html'
})
export class OrdersListComponent implements OnInit {
  public orders$: Observable<IOrdersListModel[]>;
  public dateRange: FormGroup;

  constructor(
    private fb: FormBuilder,
    private orderService: OrderService,
    private store: Store<IAppState>
  ) { }

  public ngOnInit(): void {
    this.dateRange = this.fb.group({
      'startDate': new FormControl(''),
      'endDate': new FormControl('')
    });

    this.getOrders('', '');
  }

  public getOrders(startDate: string, endDate: string): void {
    this.orderService
      .getOrders$(startDate, endDate)
      .subscribe(() => {
        this.orders$ = this.store.pipe(
          select(state => state.orders.all)
        );
      });
  }

  public filterOrders(): void {
    this.getOrders(
      this.dateRange.controls.startDate.value,
      this.dateRange.controls.endDate.value
    );
  }
}
