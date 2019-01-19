import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

import { GameDetailsModel } from '../../../models/game-details.model';
import { GameService } from '../../../services/game.service';
import { AuthService } from 'src/app/sharedServices/auth.service';
import { CartService } from '../../../services/cart.service';

@Component({
  selector: 'app-game-details',
  templateUrl: './game-details.component.html'
})
export class GameDetailsComponent implements OnInit {
  public game: GameDetailsModel = new GameDetailsModel('', '', 0, 0, '', '', '', 0, new Date);
  public videoId: SafeResourceUrl;

  constructor(
    private gameService: GameService,
    private router: ActivatedRoute,
    private sanitizer: DomSanitizer,
    private cartService: CartService,
    public authService: AuthService
  ) { }

  public ngOnInit(): void {
    this.gameService
      .getDetails(this.router.snapshot.params['id'])
      .subscribe(res => {
        this.game = res;
        this.videoId = this.sanitizer.bypassSecurityTrustResourceUrl(`https://www.youtube.com/embed/${res.videoId}`);
      });
  }

  public addItem(id: string): void {
    this.cartService.addItem(id);
  }
}
