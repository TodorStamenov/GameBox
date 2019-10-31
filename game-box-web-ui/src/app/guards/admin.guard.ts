import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';

import { AuthHelperService } from '../modules/core/services/auth-helper.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {
  constructor(
    private authHelperService: AuthHelperService,
    private router: Router
  ) { }

  public canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this.authHelperService.isAdmin) {
      return true;
    }

    this.router.navigate(['/auth/login']);
    return false;
  }
}
