import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Observable } from 'rxjs';

import { CategoryService } from 'src/app/modules/admin/services/category.service';
import { CategoryMenuModel } from 'src/app/modules/admin/models/categories/category-menu.model';
import { AuthService } from 'src/app/sharedServices/auth.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  public categories$: Observable<CategoryMenuModel[]>;

  constructor(
    private router: Router,
    public authService: AuthService,
    private categoryService: CategoryService
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  public logout(): void {
    this.authService.logout();
    this.router.navigate(['/']);
  }

  public ngOnInit(): void {
    this.categories$ = this.categoryService.getCategoryNames();
  }
}
