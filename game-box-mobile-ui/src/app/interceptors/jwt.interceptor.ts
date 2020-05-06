import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpResponse
} from '@angular/common/http';

import { Observable } from 'rxjs';

import { AuthService } from '../services/auth.service';
import { UIService } from '../services/ui.service';
import { tap } from 'rxjs/operators';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(
    private uiService: UIService,
    private authService: AuthService
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
              this.uiService.showMessage(res.body);
            } else if (res.body.message) {
              this.uiService.showMessage(res.body.message);
            }
          }
        })
      );
  }
}
