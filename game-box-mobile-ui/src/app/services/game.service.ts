import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { constants } from '~/app/common';
import { IGamesListModel } from '../modules/game/models/games-list.model';
import { IGameDetailsModel } from '../modules/game/models/game-details.model';
import { IGameCommentModel } from '../modules/game/models/game-comment.model';
import { IGameCommentBindingModel } from '../modules/game/models/game-comment-binding.model';
import { UIService } from './ui.service';

const gamesUrl = constants.host + 'games/';
const commentsUrl = constants.host + 'comments/';

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

  public getComments$(id: string): Observable<IGameCommentModel[]> {
    return this.http.get<IGameCommentModel[]>(commentsUrl + id);
  }

  public addComment$(body: IGameCommentBindingModel): Observable<void> {
    return this.http.post<void>(commentsUrl, body);
  }
}
