import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { screen } from 'tns-core-modules/platform';
import { GameService } from '../../services/game.service';
import { IGameDetailsModel } from '../../models/game-details.model';

@Component({
  selector: 'app-game-details',
  templateUrl: './game-details.component.html',
  styleUrls: ['./game-details.component.scss'],
  moduleId: module.id
})
export class GameDetailsComponent implements OnInit {
  private gameId: string;

  public loading = true;
  public embedHtml: string;
  public game: IGameDetailsModel;

  constructor(
    private route: ActivatedRoute,
    private gameService: GameService
  ) {
    this.gameId = this.route.snapshot.params['id'];
  }

  public ngOnInit(): void {
    this.gameService.getDetails$(this.gameId)
      .subscribe(game => {
        this.game = game;
        this.embedHtml = this.getHtmlEmbedTag(game.videoId);

        this.loading = false;
      });
  }

  private getHtmlEmbedTag(gameId: string): string {
    return `
      <!DOCTYPE html>
      <html>
        <body>
          <iframe
            src='https://www.youtube.com/embed/${gameId}'
            width="100%"
            height="${screen.mainScreen.widthDIPs / 1.7}"
            frameborder="0"
            allowfullscreen>
          </iframe>
        </body>
      </html>
    `;
  }
}
