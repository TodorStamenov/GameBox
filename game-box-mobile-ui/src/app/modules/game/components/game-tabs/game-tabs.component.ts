import { Component, OnInit } from '@angular/core';

import { forkJoin } from 'rxjs';
import { map } from 'rxjs/operators';

import { IGamesListModel } from '../../models/games-list.model';
import { GameService } from '../../services/game.service';
import { RouterExtensions } from 'nativescript-angular/router';

@Component({
  selector: 'app-game-tabs',
  templateUrl: './game-tabs.component.html',
  styleUrls: ['./game-tabs.component.scss'],
  moduleId: module.id
})
export class GamesTabsComponent implements OnInit {
  public loading = true;
  public games: IGamesListModel[] = [];
  public owned: IGamesListModel[] = [];

  constructor(
    private router: RouterExtensions,
    private gameService: GameService
  ) { }

  public ngOnInit(): void {
    forkJoin(
      this.gameService.getGames$(this.games.length),
      this.gameService.getGames$(this.games.length) // change to getOwned$
    ).pipe(
      map(([games, owned]) => {
        games = games.map(game => this.changeThumbnailUrls(game));
        owned = owned.map(game => this.changeThumbnailUrls(game));

        return [games, owned];
      })
    ).subscribe(([games, owned]) => {
      this.games = [...this.games, ...games];
      this.owned = [...this.owned, ...owned];

      this.loading = false;
    });
  }

  public loadGames(): void {
    this.loading = true;

    this.gameService.getGames$(this.games.length).pipe(
      map(games => games.map(game => this.changeThumbnailUrls(game)))
    ).subscribe(games => {
      this.games = [...this.games, ...games];
      this.loading = false;
    });
  }

  public loadOwned(): void {
    this.loading = true;

    this.gameService.getGames$(this.games.length).pipe(
      map(owned => owned.map(game => this.changeThumbnailUrls(game)))
    ).subscribe(owned => {
      this.owned = [...this.owned, ...owned];
      this.loading = false;
    });
  }

  public navigateToDetails(id: string): void {
    this.router.navigate(['/games/details', id], {
      animated: true,
      transition: {
        name: 'slideLeft'
      },
    });
  }

  private changeThumbnailUrls(game: IGamesListModel): IGamesListModel {
    if (!game.thumbnailUrl) {
      game.thumbnailUrl = `https://i.ytimg.com/vi/${game.videoId}/maxresdefault.jpg`;
    }

    return game;
  }
}
