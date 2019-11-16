import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { WebView } from 'tns-core-modules/ui/web-view';
import { screen } from 'tns-core-modules/platform';
import { GameService } from '../../services/game.service';
import { CartService } from '~/app/modules/cart/services/cart.service';
import { IGameDetailsModel } from '../../models/game-details.model';
import { WishlistService } from '~/app/modules/wishlist/services/wishlist.service';
import { UIService } from '~/app/modules/core/services/ui.service';

@Component({
  selector: 'ns-game-details',
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
    private uiService: UIService,
    private gameService: GameService,
    private cartService: CartService,
    private wishlistService: WishlistService
  ) {
    this.gameId = this.route.snapshot.params['id'];
  }

  public ngOnInit(): void {
    this.gameService.getDetails$(this.gameId)
      .subscribe(game => {
        this.game = game;
        this.loading = false;
        this.embedHtml = this.getHtmlEmbedTag(game.videoId);
      }, () => this.loading = false);
  }

  public onAddToCart(gameId: string): void {
    this.cartService.addItem(gameId);
    this.uiService.navigate('/cart/items');
  }

  public onAddToWishlist(gameId: string): void {
    this.wishlistService.addItem$(gameId)
      .subscribe(() => this.uiService.navigate('/wishlist/items'));
  }

  public onWebViewLoaded(webargs: any): void {
    const webview: WebView = webargs.object;
    webview.android.getSettings().setDisplayZoomControls(false);
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
