import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../modules/authentication/auth.service';
import { CategoryService } from 'src/app/modules/admin/services/category.service';
import { CategoryMenuModel } from 'src/app/modules/admin/models/categories/category-menu.model';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  public categories: CategoryMenuModel[];

  constructor(
    private router: Router,
    private authService: AuthService,
    private categoryService: CategoryService
  ) { 
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/']);
  }

  ngOnInit(): void {
    this.categoryService
      .getCategoryNames()
      .subscribe(c => this.categories = c);
  }
}