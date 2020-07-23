import { Component, Input, ChangeDetectionStrategy } from '@angular/core';

import { IGameCommentModel } from '../../models/game-comment.model';

@Component({
  selector: 'ns-game-comments',
  templateUrl: './game-comments.component.html',
  styleUrls: ['./game-comments.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  moduleId: module.id
})
export class GameCommentsComponent {
  @Input() public comments: IGameCommentModel[] = [];
}
