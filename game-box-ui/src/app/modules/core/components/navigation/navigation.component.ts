import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { CategoryService } from 'src/app/modules/admin/services/category.service';
import { AuthService } from 'src/app/sharedServices/auth.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent {
  public categories$ = this.categoryService.getCategoryNames$();

  constructor(
    public authService: AuthService,
    private router: Router,
    private categoryService: CategoryService
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  public logout(): void {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
