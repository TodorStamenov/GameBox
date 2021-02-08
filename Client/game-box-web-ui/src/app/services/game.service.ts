import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

import { IGameBindingModel } from '../modules/game/models/game-binding.model';
import { IGamesListModel } from '../modules/game/models/games-list.model';
import { IGamesHomeListModel } from '../modules/game/models/games-home-list.model';
import { IGameDetailsModel } from '../modules/game/models/game-details.model';
import { IGameCommentModel } from '../modules/game/models/game-comment.model';
import { IGameCommentBindingModel } from '../modules/game/models/game-comment-binding.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  private gamesUrl = environment.api.gamesApiUrl + 'games/';
  private commentsUrl = environment.api.gamesApiUrl + 'comments/';

  constructor(private http: HttpClient) { }

  public searchGames$(title: string = ''): Observable<IGamesListModel[]> {
    return this.http.get<IGamesListModel[]>(this.gamesUrl + 'all?title=' + title).pipe(take(1));
  }

  public getGame$(id: string): Observable<IGameBindingModel> {
    return this.http.get<IGameBindingModel>(this.gamesUrl + id).pipe(take(1));
  }

  public getGames$(loadedGames: number): Observable<IGamesHomeListModel[]> {
    return this.http.get<IGamesHomeListModel[]>(this.gamesUrl + '?loadedGames=' + loadedGames).pipe(take(1));
  }

  public getGamesByCategory$(loadedGames: number, categoryId: string): Observable<IGamesHomeListModel[]> {
    return this.http.get<IGamesHomeListModel[]>(this.gamesUrl + 'category/' + categoryId + '?loadedGames=' + loadedGames).pipe(take(1));
  }

  public getOwned$(loadedGames: number): Observable<IGamesHomeListModel[]> {
    return this.http.get<IGamesHomeListModel[]>(this.gamesUrl + 'owned?loadedGames=' + loadedGames).pipe(take(1));
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

  public deleteComment$(id: string): Observable<void> {
    return this.http.delete<void>(this.commentsUrl + id).pipe(take(1));
  }

  public addGame$(body: IGameBindingModel): Observable<void> {
    return this.http.post<void>(this.gamesUrl, body).pipe(take(1));
  }

  public editGame$(id: string, body: IGameBindingModel): Observable<void> {
    return this.http.put<void>(this.gamesUrl + id, body).pipe(take(1));
  }

  public deleteGame$(id: string): Observable<void> {
    return this.http.delete<void>(this.gamesUrl + id).pipe(take(1));
  }
}
