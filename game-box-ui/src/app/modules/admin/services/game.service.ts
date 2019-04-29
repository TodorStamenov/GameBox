import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { constants } from '../../../common';
import { IGameBindingModel } from '../models/games/game-binding.model';
import { IGamesListModel } from '../models/games/games-list.mode';

const gamesUrl = constants.host + 'games/';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  constructor(private http: HttpClient) { }

  public getGames$(title: string): Observable<IGamesListModel> {
    return this.http.get<IGamesListModel>(gamesUrl + 'all?title=' + title);
  }

  public getGame$(id: string): Observable<IGameBindingModel> {
    return this.http.get<IGameBindingModel>(gamesUrl + id);
  }

  public addGame$(body: IGameBindingModel): Observable<IGameBindingModel> {
    return this.http.post<IGameBindingModel>(gamesUrl, body);
  }

  public editGame$(id: string, body: IGameBindingModel): Observable<IGameBindingModel> {
    return this.http.put<IGameBindingModel>(gamesUrl + id, body);
  }

  public deleteGame$(id: string): Observable<IGameBindingModel> {
    return this.http.delete<IGameBindingModel>(gamesUrl + id);
  }
}
