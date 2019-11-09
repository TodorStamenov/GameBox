import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpErrorResponse
} from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { UIService } from '../modules/core/services/ui.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private uiService: UIService) { }

  public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((err: HttpErrorResponse) => {
        switch (err.status) {
          case 400:
            const message = Object.keys(err.error.errors)
              .map(e => err.error.errors[e])
              .join('\n');

            this.uiService.showMessage(message);
            break;

          case 401:
            this.uiService.showMessage(err.error.message);
            break;

          case 404:
            this.uiService.showMessage(err.error.error);
            break;

          case 500:
            if (err.error.error) {
              this.uiService.showMessage(err.error.error[0]);
            } else {
              this.uiService.showMessage(err.error);
            }
            break;
        }

        return throwError(err);
      })
    );
  }
}
