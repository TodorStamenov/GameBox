import { Component, Input, OnInit } from '@angular/core';

import { RouterExtensions } from 'nativescript-angular/router';
import { UIService } from '../services/ui.service';
import { Page } from 'tns-core-modules/ui/page';
import * as utils from 'tns-core-modules/utils/utils';

@Component({
  selector: 'ns-action-bar',
  templateUrl: './action-bar.component.html',
  styleUrls: ['./action-bar.component.scss'],
  moduleId: module.id
})
export class ActionBarComponent implements OnInit {
  @Input() public title: string;
  @Input() public showBackButton = true;

  constructor(
    private router: RouterExtensions,
    private uiService: UIService,
    private page: Page
  ) { }

  get canGoBack(): boolean {
    return this.router.canGoBack() && this.showBackButton;
  }

  public ngOnInit(): void {
    this.page.on('navigatedTo', () => {
      this.uiService.toggleDrawerGestures(!this.showBackButton);
    });
  }

  public onGoBack(): void {
    this.router.backToPreviousPage();
    this.onActionBarTap();
  }

  public onToggleMenu(): void {
    this.uiService.toggleDrawer();
    this.onActionBarTap();
  }

  public onActionBarTap(): void {
    utils.ad.dismissSoftInput();
  }
}
