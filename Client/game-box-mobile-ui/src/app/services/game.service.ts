import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';

import { IGamesListModel } from '../modules/game/models/games-list.model';
import { IGameDetailsModel } from '../modules/game/models/game-details.model';
import { IGameCommentModel } from '../modules/game/models/game-comment.model';
import { IGameCommentBindingModel } from '../modules/game/models/game-comment-binding.model';
import { UIService } from './ui.service';
import { constants } from '~/app/common';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  private gamesUrl = constants.gamesApiUrl + 'games/';
  private commentsUrl = constants.gamesApiUrl + 'comments/';

  constructor(
    private http: HttpClient,
    private uiService: UIService
  ) { }

  public getGames$(loadedGames: number): Observable<IGamesListModel[]> {
    return this.http.get<IGamesListModel[]>(this.gamesUrl + '?loadedGames=' + loadedGames).pipe(
      take(1),
      map(games => games.map(game => {
        game.thumbnailUrl = this.uiService.changeThumbnailUrls(game.thumbnailUrl, game.videoId);
        return game;
      }))
    );
  }

  public getOwned$(loadedGames: number): Observable<IGamesListModel[]> {
    return this.http.get<IGamesListModel[]>(this.gamesUrl + 'owned?loadedGames=' + loadedGames).pipe(
      take(1),
      map(games => games.map(game => {
        game.thumbnailUrl = this.uiService.changeThumbnailUrls(game.thumbnailUrl, game.videoId);
        return game;
      }))
    );
  }

  public getDetails$(id: string): Observable<IGameDetailsModel> {
    return this.http.get<IGameDetailsModel>(this.gamesUrl + 'details/' + id).pipe(take(1));
  }

  public getComments$(id: string): Observable<IGameCommentModel[]> {
    return this.http.get<IGameCommentModel[]>(this.commentsUrl + id).pipe(take(1));
  }

  public addComment$(body: IGameCommentBindingModel): Observable<void> {
    return this.http.post<void>(this.commentsUrl, body).pipe(take(1));
  }
}
