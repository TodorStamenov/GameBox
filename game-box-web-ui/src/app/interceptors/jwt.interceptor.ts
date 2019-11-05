import { Injectable } from '@angular/core';
import {
  HttpResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from '@angular/common/http';

import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { ToastrService } from 'ngx-toastr';
import { AuthHelperService } from '../modules/core/services/auth-helper.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(
    private toastrService: ToastrService,
    private authService: AuthHelperService
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
            if (res.body.token) {
              this.authService.user = res.body;
              this.toastrService.success(res.body.message, 'Success!');
            } else {
              if (typeof res.body === 'string') {
                this.toastrService.success(res.body, 'Success!');
              } else if (res.body.message) {
                this.toastrService.success(res.body.message, 'Success!');
              }
            }
          }
        })
      );
  }
}
