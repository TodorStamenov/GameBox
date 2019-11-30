import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';
import { tap, filter } from 'rxjs/operators';

import { IGameDetailsModel } from '../../models/game-details.model';
import { CartService } from '../../../cart/services/cart.service';
import { WishlistService } from 'src/app/modules/wishlist/services/wishlist.service';
import { LoadGameDetails } from 'src/app/modules/game/+store/games.actions';
import { AuthService } from 'src/app/modules/auth/services/auth.service';
import { IState } from '../../+store/games.state';

@Component({
  selector: 'app-game-details',
  templateUrl: './game-details.component.html',
  styleUrls: ['./game-details.component.scss']
})
export class GameDetailsComponent implements OnInit {
  public game$: Observable<IGameDetailsModel>;
  public videoId: SafeResourceUrl;

  private gameId: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private sanitizer: DomSanitizer,
    private cartService: CartService,
    private wishlistService: WishlistService,
    private store: Store<IState>,
    public authService: AuthService
  ) {
    this.gameId = this.route.snapshot.params['id'];
  }

  public ngOnInit(): void {
    this.store.dispatch(new LoadGameDetails(this.gameId));
    this.game$ = this.store.pipe(
      select(s => s.games.details),
      filter(g => !!g),
      tap((game: IGameDetailsModel) => {
        this.videoId = this.sanitizer.bypassSecurityTrustResourceUrl(`https://www.youtube.com/embed/${game.videoId}`);
      })
    );
  }

  public onAddItemToCart(id: string): void {
    this.cartService.addItem$(id)
      .subscribe(() => this.router.navigate(['/cart/items']));
  }

  public onAddItemToWishlist(id: string): void {
    this.wishlistService.addItem$(id)
      .subscribe(() => this.router.navigate(['/wishlist/items']));
  }
}
