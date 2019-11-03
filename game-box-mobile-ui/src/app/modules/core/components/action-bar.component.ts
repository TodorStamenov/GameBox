import { Component, Input } from '@angular/core';

import { RouterExtensions } from 'nativescript-angular/router';

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
    private router: RouterExtensions
  ) { }

  get canGoBack(): boolean {
    return this.router.canGoBack() && this.showBackButton;
  }

  public onGoBack(): void {
    this.router.backToPreviousPage();
  }
}
