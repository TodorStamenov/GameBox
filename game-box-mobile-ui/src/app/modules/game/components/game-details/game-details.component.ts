import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { WebView } from 'tns-core-modules/ui/web-view';
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

  @ViewChild('webView', { static: false })
  public webView: ElementRef;

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

        const webView: WebView = this.webView.nativeElement;
        webView.android.getSetting().setBuiltInZoomControls(false);
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
