import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-load-more',
  templateUrl: './load-more.component.html'
})
export class LoadMoreComponent {
  @Input() public gamesCount: number;
  @Output() public loadClick = new EventEmitter<void>();
}
