import { Injectable } from '@angular/core';
import {
  HttpResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from '@angular/common/http';
import { Store } from '@ngrx/store';

import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { AuthService } from '../services/auth.service';
import { IAppState } from '../store/app.state';
import { DisplayToastMessage } from '../store/+store/core/core.actions';
import { ToastType } from '../modules/core/enums/toast-type.enum';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(
    private authService: AuthService,
    private store: Store<IAppState>
  ) { }

  public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const currentUser = this.authService.user;

    if (currentUser && currentUser.token) {
      request = request.clone({
        setHeaders: {
          'Authorization': `Bearer ${currentUser.token}`,
          'Content-Type': 'application/json'
        }
      });
    }

    return next
      .handle(request)
      .pipe(
        tap((res: any) => {
          if (res instanceof HttpResponse && res.body) {
            if (typeof res.body === 'string') {
              this.store.dispatch(new DisplayToastMessage({
                message: res.body,
                toastType: ToastType.success
              }));
            } else if (res.body.message) {
              this.store.dispatch(new DisplayToastMessage({
                message: res.body.message,
                toastType: ToastType.success
              }));
            }
          }
        })
      );
  }
}
