import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';

import { CategoryService } from 'src/app/modules/category/services/category.service';
import { AuthService } from 'src/app/modules/auth/services/auth.service';
import { AuthHelperService } from '../../services/auth-helper.service';
import { ICategoryMenuModel } from 'src/app/modules/category/models/category-menu.model';
import { IAppState } from 'src/app/store/app.state';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  public categories$: Observable<ICategoryMenuModel[]>;

  constructor(
    public authHelperService: AuthHelperService,
    private authService: AuthService,
    private router: Router,
    private categoryService: CategoryService,
    private store: Store<IAppState>
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  public ngOnInit(): void {
    this.categoryService
      .getCategoryNames$()
      .subscribe(() => {
        this.categories$ = this.store.pipe(
          select(state => state.categories.names)
        );
      });
  }

  public logout(): void {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
