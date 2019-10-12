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
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private toastr: ToastrService) { }

  public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next
      .handle(req)
      .pipe(catchError((err: HttpErrorResponse) => {
        switch (err.status) {
          case 400:
            const message = Object.keys(err.error.errors)
              .map(e => err.error.errors[e])
              .join('\n');

            this.toastr.error(message, 'Warning!');
            break;

          case 401:
            this.toastr.error(err.error.message, 'Warning!');
            break;

          case 404:
            this.toastr.error(err.error.error, 'Warning!');
            break;

          case 500:
            if (err.error.error) {
              this.toastr.error(err.error.error, 'Warning!');
            } else {
              this.toastr.error(err.error, 'Warning!');
            }
            break;
        }

        return throwError(err);
      }));
  }
}
