import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { AuthService } from 'src/app/services/auth.service';
import { NotificationsService } from 'src/app/services/notifications.service';
import { ICategoryMenuModel } from 'src/app/modules/category/models/category-menu.model';
import { IAppState } from 'src/app/store/app.state';
import { LoadCategoryNames } from 'src/app/store/+store/category/categories.actions';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html'
})
export class NavigationComponent implements OnInit {
  public categories$: Observable<ICategoryMenuModel[]>;

  constructor(
    public authService: AuthService,
    private notificationService: NotificationsService,
    private router: Router,
    private store: Store<IAppState>
  ) {
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  public ngOnInit(): void {
    this.store.dispatch(new LoadCategoryNames());
    this.categories$ = this.store.pipe(
      select(s => s.categories.names),
      map(categories => categories.filter(c => c.hasGames))
    );

    if (this.authService.isAuthed) {
      this.notificationService.subscribeForNotifications();
    }
  }

  public onLogout(): void {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
