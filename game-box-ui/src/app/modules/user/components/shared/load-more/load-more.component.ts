import {
  Component,
  Input,
  Output,
  EventEmitter,
  OnInit,
  OnDestroy,
  ChangeDetectionStrategy,
  ChangeDetectorRef
} from '@angular/core';

@Component({
  selector: 'app-load-more',
  templateUrl: './load-more.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoadMoreComponent implements OnInit, OnDestroy {
  public showButton = false;
  private id: any;

  @Input() public gamesCount: number;
  @Output() public loadClick = new EventEmitter<void>();

  constructor(private cdr: ChangeDetectorRef) { }

  public ngOnInit(): void {
    this.id = setTimeout(() => {
      this.showButton = true;
      this.cdr.markForCheck();
    }, 500);
  }

  public ngOnDestroy(): void {
    clearTimeout(this.id);
  }
}
