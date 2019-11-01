import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
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
    private router: Router,
    private toastr: ToastrService,
    private authService: AuthHelperService
  ) { }

  public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const currentUser = JSON.parse(localStorage.getItem('currentUser'));

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
          if (res instanceof HttpResponse
            && res.body
            && res.body.token
            && (res.url.endsWith('login')
              || res.url.endsWith('register'))) {
            this.saveToken(res.body);
            this.toastr.success(res.body.message, 'Success!');
            this.router.navigate(['/']);
          }

          if (res instanceof HttpResponse
            && res.body
            && res.body.message
            && !res.url.endsWith('login')
            && !res.url.endsWith('register')) {
            this.toastr.success(res.body.message, 'Success!');
          }

          if (res instanceof HttpResponse
            && res.body
            && !res.url.endsWith('login')
            && !res.url.endsWith('register')
            && typeof res.body === 'string') {
            this.toastr.success(res.body, 'Success!');
          }
        })
      );
  }

  private saveToken(data: any): void {
    this.authService.user = data;
  }
}
