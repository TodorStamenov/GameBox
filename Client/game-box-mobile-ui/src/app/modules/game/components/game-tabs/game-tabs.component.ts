import { Component, OnInit, OnDestroy } from '@angular/core';

import { forkJoin } from 'rxjs';
import { takeWhile } from 'rxjs/operators';

import { IGamesListModel } from '../../models/games-list.model';
import { GameService } from '../../../../services/game.service';
import { UIService } from '~/app/services/ui.service';

@Component({
  selector: 'ns-game-tabs',
  templateUrl: './game-tabs.component.html',
  styleUrls: ['./game-tabs.component.scss'],
  moduleId: module.id
})
export class GamesTabsComponent implements OnInit, OnDestroy {
  private isActive = true;

  public loading = true;
  public games: IGamesListModel[] = [];
  public owned: IGamesListModel[] = [];

  constructor(
    private uiService: UIService,
    private gameService: GameService
  ) { }

  public ngOnInit(): void {
   this.reloadGames();
  }

  public ngOnDestroy(): void {
    this.isActive = false;
  }

  public loadGames(): void {
    this.gameService.getGames$(this.games.length).pipe(
      takeWhile(() => this.isActive)
    ).subscribe(
      games => this.games = [...this.games, ...games],
      () => this.loading = false
    );
  }

  public loadOwned(): void {
    this.gameService.getOwned$(this.owned.length).pipe(
      takeWhile(() => this.isActive)
    ).subscribe(
      owned => this.owned = [...this.owned, ...owned],
      () => this.loading = false
    );
  }

  public navigateToDetails(id: string): void {
    this.uiService.navigate('/games/details', [id]);
  }

  public reloadGames(): void {
    this.loading = true;

    this.games = [];
    this.owned = [];

    forkJoin(
      this.gameService.getGames$(this.games.length),
      this.gameService.getOwned$(this.owned.length)
    ).pipe(
      takeWhile(() => this.isActive)
    ).subscribe(([games, owned]) => {
      this.games = [...this.games, ...games];
      this.owned = [...this.owned, ...owned];

      this.loading = false;
    }, () => this.loading = false);
  }
}
