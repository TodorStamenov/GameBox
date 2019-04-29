import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { constants } from 'src/app/common';
import { IGamesListModel } from '../models/games/games-list.model';
import { IGameDetailsModel } from '../models/games/game-details.model';

const gamesUrl = constants.host + 'games/';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  constructor(private http: HttpClient) { }

  public getGames$(loadedGames: number, categoryId: string): Observable<IGamesListModel[]> {
    return this.http.get<IGamesListModel[]>(gamesUrl + '?loadedGames=' + loadedGames + '&categoryId=' + categoryId);
  }

  public getOwned$(loadedGames: number): Observable<IGamesListModel[]> {
    return this.http.get<IGamesListModel[]>(gamesUrl + 'owned?loadedGames=' + loadedGames);
  }

  public getDetails$(id: string): Observable<IGameDetailsModel> {
    return this.http.get<IGameDetailsModel>(gamesUrl + 'details/' + id);
  }
}
