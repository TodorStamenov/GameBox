import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';

import { Observable } from 'rxjs';

import { IOrdersListModel } from '../../models/orders-list.model';
import { IAppState } from 'src/app/store/app.state';
import { LoadAllOrders } from 'src/app/store/orders/orders.actions';

@Component({
  selector: 'app-orders-list',
  templateUrl: './orders-list.component.html'
})
export class OrdersListComponent implements OnInit {
  public orders$: Observable<IOrdersListModel[]>;
  public dateRange: FormGroup;

  constructor(
    private fb: FormBuilder,
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
    this.store.dispatch(new LoadAllOrders({ startDate, endDate }));
    this.orders$ = this.store.pipe(
      select(s => s.orders.all)
    );
  }

  public filterOrders(): void {
    this.getOrders(
      this.dateRange.controls.startDate.value,
      this.dateRange.controls.endDate.value
    );
  }
}
