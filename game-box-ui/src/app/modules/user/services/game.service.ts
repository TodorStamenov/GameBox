import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { constants } from 'src/app/common';
import { IListGamesModel } from '../models/list-games.model';
import { IGameDetailsModel } from '../models/game-details.model';

const gamesUrl = constants.host + 'games/';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  constructor(private http: HttpClient) { }

  public getGames$(loadedGames: number, categoryId: string): Observable<IListGamesModel[]> {
    return this.http.get<IListGamesModel[]>(gamesUrl + '?loadedGames=' + loadedGames + '&categoryId=' + categoryId);
  }

  public getOwned$(loadedGames: number): Observable<IListGamesModel[]> {
    return this.http.get<IListGamesModel[]>(gamesUrl + 'owned?loadedGames=' + loadedGames);
  }

  public getDetails$(id: string): Observable<IGameDetailsModel> {
    return this.http.get<IGameDetailsModel>(gamesUrl + 'details/' + id);
  }
}
