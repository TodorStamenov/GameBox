import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

import { CategoryService } from 'src/app/modules/admin/services/category.service';
import { AuthService } from 'src/app/modules/auth/services/auth.service';
import { AuthHelperService } from '../../services/auth-helper.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit, OnDestroy {
  private id: any;
  public categories$ = this.categoryService.getCategoryNames$();

  constructor(
    public authHelperService: AuthHelperService,
    private authService: AuthService,
    private router: Router,
    private categoryService: CategoryService
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  public ngOnInit(): void {
    this.id = setInterval(() => {
      this.categories$ = this.categoryService.getCategoryNames$();
    }, 60 * 60 * 24);
  }

  public ngOnDestroy(): void {
    clearInterval(this.id);
  }

  public logout(): void {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
