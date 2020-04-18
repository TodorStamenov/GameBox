import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { IGameBindingModel } from '../models/game-binding.model';
import { IGamesListModel } from '../models/games-list.model';
import { IGamesHomeListModel } from '../models/games-home-list.model';
import { IGameDetailsModel } from '../models/game-details.model';
import { IGameCommentModel } from '../models/game-comment.model';
import { IGameCommentBindingModel } from '../models/game-comment-binding.model';
import { environment } from 'src/environments/environment';

const gamesUrl = environment.api.baseUrl + 'games/';
const commentsUrl = environment.api.baseUrl + 'comments/';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  constructor(private http: HttpClient) { }

  public searchGames$(title: string = ''): Observable<IGamesListModel[]> {
    return this.http.get<IGamesListModel[]>(gamesUrl + 'all?title=' + title);
  }

  public getGame$(id: string): Observable<IGameBindingModel> {
    return this.http.get<IGameBindingModel>(gamesUrl + id);
  }

  public getGames$(loadedGames: number): Observable<IGamesHomeListModel[]> {
    return this.http.get<IGamesHomeListModel[]>(gamesUrl + '?loadedGames=' + loadedGames);
  }

  public getGamesByCategory$(loadedGames: number, categoryId: string): Observable<IGamesHomeListModel[]> {
    return this.http.get<IGamesHomeListModel[]>(gamesUrl + 'category/' + categoryId + '?loadedGames=' + loadedGames);
  }

  public getOwned$(loadedGames: number): Observable<IGamesHomeListModel[]> {
    return this.http.get<IGamesHomeListModel[]>(gamesUrl + 'owned?loadedGames=' + loadedGames);
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

  public deleteComment$(id: string): Observable<void> {
    return this.http.delete<void>(commentsUrl + id);
  }

  public addGame$(body: IGameBindingModel): Observable<void> {
    return this.http.post<void>(gamesUrl, body);
  }

  public editGame$(id: string, body: IGameBindingModel): Observable<void> {
    return this.http.put<void>(gamesUrl + id, body);
  }

  public deleteGame$(id: string): Observable<void> {
    return this.http.delete<void>(gamesUrl + id);
  }
}
