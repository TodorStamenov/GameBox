import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { constants } from '~/app/common';
import { IGamesListModel } from '../models/games-list.model';
import { IGameDetailsModel } from '../models/game-details.model';
import { UIService } from '../../core/services/ui.service';

const gamesUrl = constants.host + 'games/';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  constructor(
    private http: HttpClient,
    private uiService: UIService
  ) { }

  public getGames$(loadedGames: number): Observable<IGamesListModel[]> {
    return this.http.get<IGamesListModel[]>(gamesUrl + '?loadedGames=' + loadedGames).pipe(
      map(games => games.map(game => {
        game.thumbnailUrl = this.uiService.changeThumbnailUrls(game.thumbnailUrl, game.videoId);
        return game;
      }))
    );
  }

  public getOwned$(loadedGames: number): Observable<IGamesListModel[]> {
    return this.http.get<IGamesListModel[]>(gamesUrl + 'owned?loadedGames=' + loadedGames).pipe(
      map(games => games.map(game => {
        game.thumbnailUrl = this.uiService.changeThumbnailUrls(game.thumbnailUrl, game.videoId);
        return game;
      }))
    );
  }

  public getDetails$(id: string): Observable<IGameDetailsModel> {
    return this.http.get<IGameDetailsModel>(gamesUrl + 'details/' + id);
  }
}
