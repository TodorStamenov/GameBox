import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import { Screen, WebView, Utils } from '@nativescript/core';

import { forkJoin } from 'rxjs';
import { takeWhile, switchMap } from 'rxjs/operators';

import { GameService } from '../../../../services/game.service';
import { CartService } from '~/app/services/cart.service';
import { IGameDetailsModel } from '../../models/game-details.model';
import { WishlistService } from '~/app/services/wishlist.service';
import { UIService } from '~/app/services/ui.service';
import { IGameCommentModel } from '../../models/game-comment.model';

@Component({
  selector: 'ns-game-details',
  templateUrl: './game-details.component.html',
  styleUrls: ['./game-details.component.scss'],
  moduleId: module.id
})
export class GameDetailsComponent implements OnInit, OnDestroy {
  private gameId: string;
  private isActive = true;

  public loading = true;
  public showCommentForm = false;
  public embedHtml: string;
  public commentForm: FormGroup;

  public game: IGameDetailsModel;
  public comments: IGameCommentModel[];

  constructor(
    private route: ActivatedRoute,
    private uiService: UIService,
    private gameService: GameService,
    private cartService: CartService,
    private wishlistService: WishlistService
  ) {
    this.gameId = this.route.snapshot.params['id'];
  }

  get content(): AbstractControl {
    return this.commentForm.get('content');
  }

  public ngOnInit(): void {
    forkJoin([
      this.gameService.getDetails$(this.gameId),
      this.gameService.getComments$(this.gameId)
    ]).pipe(
      takeWhile(() => this.isActive)
    ).subscribe(([game, comments]) => {
      this.game = game;
      this.embedHtml = this.getHtmlEmbedTag(game.videoId);
      this.comments = comments;
      this.loading = false;
    }, () => this.loading = false);

    this.commentForm = new FormGroup({
      'gameId': new FormControl(this.gameId),
      'content': new FormControl(null, {
        updateOn: 'change',
        validators: [Validators.required, Validators.minLength(3)]
      })
    });
  }

  public ngOnDestroy(): void {
    this.isActive = false;
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

  public dismissKeyboard(): void {
    Utils.ad.dismissSoftInput();
  }

  public onToggleCommentForm(): void {
    this.showCommentForm = !this.showCommentForm;
  }

  public onSaveComment(): void {
    console.log(this.content);

    if (this.content.invalid) {
      this.uiService.showMessage('Comment content should be at least 3 symbols');
      return;
    }

    this.dismissKeyboard();
    this.showCommentForm = false;

    this.gameService.addComment$(this.commentForm.value).pipe(
      takeWhile(() => this.isActive),
      switchMap(() => this.gameService.getComments$(this.gameId))
    )
    .subscribe(comments => {
      this.comments = comments;
      this.commentForm.reset();
      this.commentForm.setValue({
        gameId: this.gameId,
        content: null
      });
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
            height="${Screen.mainScreen.widthDIPs / 1.7}"
            frameborder="0"
            allowfullscreen>
          </iframe>
        </body>
      </html>
    `;
  }
}
