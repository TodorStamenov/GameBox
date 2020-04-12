import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';

import { IAppState } from 'src/app/store/app.state';
import { IToastModel } from '../../models/toast.model';
import { tap } from 'rxjs/operators';
import { HideToastMessage } from 'src/app/store/+store/core/core.actions';
import { ToastType } from '../../enums/toast-type.enum';

@Component({
  selector: 'app-toast',
  templateUrl: './toast.component.html'
})
export class ToastComponent implements OnInit {
  public toastType = ToastType;
  public toast$: Observable<IToastModel>;

  constructor(private store: Store<IAppState>) { }

  public ngOnInit(): void {
    this.toast$ = this.store.pipe(
      select(s => s.core.toast),
      tap(s => {
        if (s) {
          setTimeout(() => {
            this.store.dispatch(new HideToastMessage());
          }, 3000);
        }
      })
    );
  }
}
