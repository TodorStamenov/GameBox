import { Component, Input, Output, EventEmitter, OnInit, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-load-more',
  templateUrl: './load-more.component.html'
})
export class LoadMoreComponent implements OnInit, OnDestroy  {
  public showButton = false;
  private id: any;

  @Input() public gamesCount: number;
  @Output() public loadClick = new EventEmitter<void>();

  ngOnInit () {
    this.id = setTimeout(() => this.showButton = true, 500);
  }

  ngOnDestroy () {
    clearTimeout(this.id);
  }
}
