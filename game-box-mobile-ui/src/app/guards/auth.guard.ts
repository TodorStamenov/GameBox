import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { AuthService } from '../modules/auth/services/auth.service';
import { RouterExtensions } from 'nativescript-angular/router';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private router: RouterExtensions
  ) { }

  public canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this.authService.isAuthed) {
      return true;
    }

    this.router.navigate(['/auth/login'], {
      clearHistory: true,
      transition: { name: 'slideLeft' }
    });

    return false;
  }
}
