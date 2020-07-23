import {
  Component,
  OnInit,
  OnDestroy,
  ViewChild,
  AfterViewInit,
  ChangeDetectorRef
} from '@angular/core';

import { takeWhile } from 'rxjs/operators';

import { UIService } from './services/ui.service';
import { RadSideDrawerComponent } from 'nativescript-ui-sidedrawer/angular/side-drawer-directives';
import { RadSideDrawer } from 'nativescript-ui-sidedrawer';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'ns-app',
  templateUrl: './app.component.html',
  moduleId: module.id
})
export class AppComponent implements OnInit, AfterViewInit, OnDestroy {
  private active = true;
  private drawer: RadSideDrawer;

  @ViewChild(RadSideDrawerComponent, { static: false })
  public drawerCompnent: RadSideDrawerComponent;

  constructor(
    private uiService: UIService,
    private cdr: ChangeDetectorRef,
    public authService: AuthService
  ) { }

  public ngOnInit(): void {
    this.uiService.drawerState$.pipe(
      takeWhile(() => this.active)
    ).subscribe(() => {
      if (this.drawer) {
        this.drawer.toggleDrawerState();
      }
    });

    this.uiService.drawerGestures$.pipe(
      takeWhile(() => this.active)
    ).subscribe(state => {
      if (this.drawer) {
        this.drawer.gesturesEnabled = state;
      }
    });
  }

  public ngAfterViewInit(): void {
    this.drawer = this.drawerCompnent.sideDrawer;
    this.cdr.detectChanges();
  }

  public onChangePassword(): void {
    this.navigate('/auth/change-password');
  }

  public ngOnDestroy(): void {
    this.active = false;
  }

  public onLogin(): void {
    this.navigate('/auth/login', true);
  }

  public onRegister(): void {
    this.navigate('/auth/register', true);
  }

  public onWishlist(): void {
    this.navigate('/wishlist/items');
  }

  public onCart(): void {
    this.navigate('/cart/items');
  }

  public onLogout(): void {
    this.authService.logout();
    this.navigate('/auth/login', true);
  }

  private navigate(path: string, clearHistory = false): void {
    this.drawer.closeDrawer();
    this.uiService.navigate(path, [], clearHistory);
  }
}
