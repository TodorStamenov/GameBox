import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {
  HttpResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from '@angular/common/http';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(
    private toastr: ToastrService,
    private router: Router
  ) { }

  public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const currentUser = JSON.parse(localStorage.getItem('currentUser'));

    if (currentUser && currentUser.token) {
      request = request.clone({
        setHeaders: {
          'Authorization': `Bearer ${currentUser.token}`
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
        })
      );
  }

  private saveToken(data: any): void {
    localStorage.setItem('currentUser', JSON.stringify({
      username: data.username,
      token: data.token,
      isAdmin: data.isAdmin
    }));
  }
}
