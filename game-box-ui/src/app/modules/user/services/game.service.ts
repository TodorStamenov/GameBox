import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { constants } from 'src/app/common';
import { ListGamesModel } from '../models/list-games.model';
import { GameDetailsModel } from '../models/game-details.model';

const gamesUrl = constants.host + 'games/';

@Injectable()
export class GameService {
  constructor(private http: HttpClient) { }

  public getGames(loadedGames: number, categoryId: string): Observable<ListGamesModel[]> {
    return this.http.get<ListGamesModel[]>(gamesUrl + '?loadedGames=' + loadedGames + '&categoryId=' + categoryId);
  }

  public getOwned(loadedGames: number): Observable<ListGamesModel[]> {
    return this.http.get<ListGamesModel[]>(gamesUrl + 'owned?loadedGames=' + loadedGames);
  }

  public getDetails(id: string): Observable<GameDetailsModel> {
    return this.http.get<GameDetailsModel>(gamesUrl + 'details/' + id);
  }
}
