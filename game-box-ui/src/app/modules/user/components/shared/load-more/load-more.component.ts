import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';

@Component({
  selector: 'app-load-more',
  templateUrl: './load-more.component.html'
})
export class LoadMoreComponent implements OnInit  {
  public showButton = false;

  @Input() public gamesCount: number;
  @Output() public loadClick = new EventEmitter<void>();

  ngOnInit () {
    setTimeout(() => this.showButton = true, 500);
  }
}
