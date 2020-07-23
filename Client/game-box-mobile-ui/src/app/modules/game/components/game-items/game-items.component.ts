import {
  Component,
  Input,
  Output,
  ChangeDetectionStrategy,
  EventEmitter,
  OnInit,
  OnDestroy
} from '@angular/core';

import { Subject } from 'rxjs';
import { takeWhile } from 'rxjs/operators';

import { IGamesListModel } from '../../models/games-list.model';

import { registerElement } from 'nativescript-angular/element-registry';
registerElement('PullToRefresh', () => require('@nstudio/nativescript-pulltorefresh').PullToRefresh);

@Component({
  selector: 'ns-game-items',
  templateUrl: './game-items.component.html',
  styleUrls: ['./game-items.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  moduleId: module.id
})
export class GameItemsComponent implements OnInit, OnDestroy {
  private isActive = true;
  private loadMore$$ = new Subject<void>();
  private gamesLength = 0;

  @Input() public games: IGamesListModel[] = [];

  @Output() public loadTap = new EventEmitter<void>();
  @Output() public pullToRefresh = new EventEmitter<void>();
  @Output() public detailsTap = new EventEmitter<string>();

  public ngOnInit(): void {
    this.loadMore$$.asObservable().pipe(
      takeWhile(() => this.isActive && this.gamesLength !== this.games.length)
    ).subscribe(() => {
      this.loadTap.emit();
      this.gamesLength = this.games.length;
    });
  }

  public ngOnDestroy(): void {
    this.isActive = false;
  }

  public onPullToRefresh(): void {
    this.pullToRefresh.emit();
  }

  public onNavigateToDetails(id: string): void {
    this.detailsTap.emit(id);
  }

  public onLoadMoreGames(): void {
    this.loadMore$$.next();
  }
}
