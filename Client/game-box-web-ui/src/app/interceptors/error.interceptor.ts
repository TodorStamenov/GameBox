import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpErrorResponse
} from '@angular/common/http';
import { Store } from '@ngrx/store';

import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { IAppState } from '../store/app.state';
import { DisplayToastMessage } from '../store/+store/core/core.actions';
import { ToastType } from '../modules/core/enums/toast-type.enum';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private store: Store<IAppState>) { }

  public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((err: HttpErrorResponse) => {
        switch (err.status) {
          case 400:
            const message = Object.keys(err.error.errors)
              .map(e => err.error.errors[e])
              .join('\n');

            this.store.dispatch(new DisplayToastMessage({
              message,
              toastType: ToastType.failure
            }));
            break;

          case 401:
            this.store.dispatch(new DisplayToastMessage({
              message: err.error.message,
              toastType: ToastType.failure
            }));
            break;

          case 404:
            this.store.dispatch(new DisplayToastMessage({
              message: err.error.error,
              toastType: ToastType.failure
            }));
            break;

          case 500:
            if (err.error.error) {
              this.store.dispatch(new DisplayToastMessage({
                message: err.error.error,
                toastType: ToastType.failure
              }));
            } else {
              this.store.dispatch(new DisplayToastMessage({
                message: err.error,
                toastType: ToastType.failure
              }));
            }
            break;
        }

        return throwError(err);
      })
    );
  }
}
