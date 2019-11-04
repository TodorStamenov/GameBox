import { Component, Input } from '@angular/core';

import { RouterExtensions } from 'nativescript-angular/router';
import { UIService } from '../services/ui.service';

@Component({
  selector: 'ns-action-bar',
  templateUrl: './action-bar.component.html',
  styleUrls: ['./action-bar.component.scss'],
  moduleId: module.id
})
export class ActionBarComponent {
  @Input() public title: string;
  @Input() public showBackButton = true;

  constructor(
    private router: RouterExtensions,
    private uiService: UIService
  ) { }

  get canGoBack(): boolean {
    return this.router.canGoBack() && this.showBackButton;
  }

  public onGoBack(): void {
    this.router.backToPreviousPage();
  }

  public onToggleMenu(): void {
    this.uiService.toggleDrawer();
  }
}
