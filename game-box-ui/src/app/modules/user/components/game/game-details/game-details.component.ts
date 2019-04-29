import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

import { Observable } from 'rxjs';

import { IGameDetailsModel } from '../../../models/game-details.model';
import { GameService } from '../../../services/game.service';
import { CartService } from '../../../services/cart.service';
import { AuthHelperService } from 'src/app/modules/core/services/auth-helper.service';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-game-details',
  templateUrl: './game-details.component.html',
  styleUrls: ['./game-details.component.css']
})
export class GameDetailsComponent implements OnInit {
  public game$: Observable<IGameDetailsModel>; // = new IGameDetailsModel('', '', 0, 0, '', '', '', 0, new Date);
  public videoId: SafeResourceUrl;
  private gameId: string;

  constructor(
    private gameService: GameService,
    private router: ActivatedRoute,
    private sanitizer: DomSanitizer,
    private cartService: CartService,
    public authHelperService: AuthHelperService
  ) {
    this.gameId = this.router.snapshot.params['id'];
  }

  public ngOnInit(): void {
    this.game$ = this.gameService
      .getDetails$(this.gameId)
      .pipe(
        tap((res: IGameDetailsModel) => {
          this.videoId = this.sanitizer.bypassSecurityTrustResourceUrl(`https://www.youtube.com/embed/${res.videoId}`);
        })
      );
  }

  public addItem(id: string): void {
    this.cartService.addItem(id);
  }
}
