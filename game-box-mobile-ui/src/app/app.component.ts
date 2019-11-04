import {
  Component,
  OnInit,
  OnDestroy,
  ViewChild,
  AfterViewInit,
  ChangeDetectorRef
} from '@angular/core';

import { takeWhile } from 'rxjs/operators';

import { UIService } from './modules/core/services/ui.service';
import { RadSideDrawerComponent } from 'nativescript-ui-sidedrawer/angular/side-drawer-directives';
import { RadSideDrawer } from 'nativescript-ui-sidedrawer';

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
    private cdr: ChangeDetectorRef
  ) { }

  public ngOnInit(): void {
    this.uiService.drawerState$.pipe(
      takeWhile(() => this.active)
    ).subscribe(() => {
      if (this.drawer) {
        this.drawer.toggleDrawerState();
      }
    });
  }

  public ngAfterViewInit(): void {
    this.drawer = this.drawerCompnent.sideDrawer;
    this.cdr.detectChanges();
  }

  public ngOnDestroy(): void {
    this.active = false;
  }
}
