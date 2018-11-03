import { Component, OnInit } from '@angular/core';
import { GameDetailsModel } from '../../../models/game-details.model';
import { GameService } from '../../../services/game.service';
import { ActivatedRoute } from '@angular/router';
import { DomSanitizer } from '@angular/platform-browser';
import { AuthService } from 'src/app/modules/authentication/auth.service';

@Component({
  selector: 'app-game-details',
  templateUrl: './game-details.component.html'
})
export class GameDetailsComponent implements OnInit {
  public game: GameDetailsModel = new GameDetailsModel();
  public videoId;

  constructor(
    private gameService: GameService,
    private router: ActivatedRoute,
    private sanitizer: DomSanitizer,
    private authService: AuthService
  ) { }

  ngOnInit() {
    this.gameService
      .getDetails(this.router.snapshot.params['id'])
      .subscribe(res => {
        this.game = res;
        this.videoId = this.sanitizer.bypassSecurityTrustResourceUrl(`https://www.youtube.com/embed/${res.videoId}`);
      });
  }
}