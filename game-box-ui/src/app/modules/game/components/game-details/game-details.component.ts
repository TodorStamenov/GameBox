import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';

import { IGameDetailsModel } from '../../models/game-details.model';
import { CartService } from '../../../cart/services/cart.service';
import { AuthHelperService } from 'src/app/modules/core/services/auth-helper.service';
import { tap } from 'rxjs/operators';
import { IAppState } from 'src/app/store/app.state';
import { GameService } from '../../services/game.service';

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
    private gameService: GameService,
    private router: ActivatedRoute,
    private sanitizer: DomSanitizer,
    private cartService: CartService,
    private store: Store<IAppState>,
    public authHelperService: AuthHelperService
  ) {
    this.gameId = this.router.snapshot.params['id'];
  }

  public ngOnInit(): void {
    this.gameService
      .getDetails$(this.gameId)
      .subscribe(() => {
        this.game$ = this.store.pipe(
          select(state => state.games.detail),
          tap((game: IGameDetailsModel) => {
            this.videoId = this.sanitizer.bypassSecurityTrustResourceUrl(`https://www.youtube.com/embed/${game.videoId}`);
          })
        );
      });
  }

  public addItem(id: string): void {
    this.cartService.addItem(id);
  }
}
