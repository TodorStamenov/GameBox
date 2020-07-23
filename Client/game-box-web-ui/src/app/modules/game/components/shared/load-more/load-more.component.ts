import { Component, Output, EventEmitter, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-load-more',
  templateUrl: './load-more.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoadMoreComponent {
  @Output() public loadClick = new EventEmitter<void>();

  public onLoadClick(): void {
    this.loadClick.emit();
  }
}
