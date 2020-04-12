import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';

import { Observable, merge } from 'rxjs';

import { IAppState } from 'src/app/store/app.state';
import { IToastModel } from '../../models/toast.model';
import { filter, tap, ignoreElements, debounceTime } from 'rxjs/operators';
import { ToastType } from '../../enums/toast-type.enum';
import { HideToastMessage } from 'src/app/store/+store/core/core.actions';

@Component({
  selector: 'app-toast',
  templateUrl: './toast.component.html'
})
export class ToastComponent implements OnInit {
  public toastType = ToastType;
  public toast$: Observable<IToastModel>;

  constructor(private store: Store<IAppState>) { }

  public ngOnInit(): void {
    const outer = this.store.pipe(
      select(t => t.core.toast)
    );

    const inner = outer.pipe(
      debounceTime(3000),
      filter(t => !!t),
      tap(() => this.store.dispatch(new HideToastMessage())),
      ignoreElements()
    );

    this.toast$ = merge(outer, inner);
  }
}
