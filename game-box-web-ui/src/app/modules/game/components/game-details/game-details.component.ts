import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';
import { tap, filter } from 'rxjs/operators';

import { IGameDetailsModel } from '../../models/game-details.model';
import { IGameCommentModel } from '../../models/game-comment.model';
import { CartService } from '../../../cart/services/cart.service';
import { GameService } from '../../services/game.service';
import { WishlistService } from 'src/app/modules/wishlist/services/wishlist.service';
import { LoadGameDetails, LoadGameComments } from 'src/app/modules/game/+store/games.actions';
import { AuthService } from 'src/app/modules/auth/services/auth.service';
import { FormService } from 'src/app/modules/core/services/form.service';
import { IState } from '../../+store/games.state';

@Component({
  selector: 'app-game-details',
  templateUrl: './game-details.component.html',
  styleUrls: ['./game-details.component.scss']
})
export class GameDetailsComponent implements OnInit {
  public showCommentForm = false;

  public game$: Observable<IGameDetailsModel>;
  public comments$: Observable<IGameCommentModel[]>;

  public videoId: SafeResourceUrl;
  public commentForm: FormGroup;

  private gameId: string;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private sanitizer: DomSanitizer,
    private gameService: GameService,
    private cartService: CartService,
    private wishlistService: WishlistService,
    private store: Store<IState>,
    public formService: FormService,
    public authService: AuthService
  ) {
    this.gameId = this.route.snapshot.params['id'];
  }

  get content(): AbstractControl {
    return this.commentForm.get('content');
  }

  public ngOnInit(): void {
    this.store.dispatch(new LoadGameDetails(this.gameId));
    this.store.dispatch(new LoadGameComments(this.gameId));

    this.game$ = this.store.pipe(
      select(s => s.games.details),
      filter(g => !!g),
      tap((game: IGameDetailsModel) => {
        this.videoId = this.sanitizer.bypassSecurityTrustResourceUrl(`https://www.youtube.com/embed/${game.videoId}`);
      })
    );

    this.comments$ = this.store.pipe(
      select(s => s.games.comments)
    );

    this.commentForm = this.fb.group({
      'gameId': this.gameId,
      'content': [null, [Validators.required, Validators.minLength(3)]]
    });
  }

  public onAddItemToCart(id: string): void {
    this.cartService.addItem$(id)
      .subscribe(() => this.router.navigate(['/cart/items']));
  }

  public onAddItemToWishlist(id: string): void {
    this.wishlistService.addItem$(id)
      .subscribe(() => this.router.navigate(['/wishlist/items']));
  }

  public onToggleCommentForm(): void {
    this.showCommentForm = !this.showCommentForm;
  }

  public saveComment(): void {
    this.showCommentForm = false;

    this.gameService.addComment$(this.commentForm.value)
      .subscribe(() => {
        this.store.dispatch(new LoadGameComments(this.gameId));
        this.commentForm.reset();
        this.commentForm.setValue({
          gameId: this.gameId,
          content: null
        });
      });
  }

  public onDeleteClick(id: string): void {
    this.gameService.deleteComment$(id)
      .subscribe(() => this.store.dispatch(new LoadGameComments(this.gameId)));
  }
}
