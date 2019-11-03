import {
  Component,
  Input,
  ChangeDetectionStrategy,
  Output,
  EventEmitter
} from '@angular/core';

import { IGamesListModel } from '../../models/games-list.model';

@Component({
  selector: 'app-game-items',
  templateUrl: './game-items.component.html',
  styleUrls: ['./game-items.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  moduleId: module.id
})
export class GameItemsComponent {
  @Input() public games: IGamesListModel[] = [];
  @Output() public loadTap = new EventEmitter<void>();
  @Output() public detailsTap = new EventEmitter<string>();

  public onNavigateToDetails(id: string): void {
    this.detailsTap.emit(id);
  }

  public onLoadMoreGames(): void {
    this.loadTap.emit();
  }
}
