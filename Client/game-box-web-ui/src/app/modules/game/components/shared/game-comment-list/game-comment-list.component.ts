import { Component, Input, Output, EventEmitter, ChangeDetectionStrategy } from '@angular/core';

import { IGameCommentModel } from '../../../models/game-comment.model';

@Component({
  selector: 'app-game-comment-list',
  templateUrl: './game-comment-list.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class GameCommentListComponent {
  @Input() public isAdmin: boolean;
  @Input() public comments: IGameCommentModel[];

  @Output() public deleteClick = new EventEmitter<string>();

  public onDeletelick(id: string): void {
    this.deleteClick.emit(id);
  }
}
