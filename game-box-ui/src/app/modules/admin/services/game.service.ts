import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { constants } from '../../../common';
import { GameBindingModel } from '../models/games/game-binding.model';
import { Observable } from 'rxjs';

const gamesUrl = constants.host + 'games/';

@Injectable()
export class GameService {
  constructor(private http: HttpClient) { }

  public getGame(id: string): Observable<GameBindingModel> {
    return this.http.get<GameBindingModel>(gamesUrl + id);
  }

  public addGame(body: GameBindingModel): Observable<GameBindingModel> {
    return this.http.post<GameBindingModel>(gamesUrl, body);
  }

  public editGame(id: string, body: GameBindingModel): Observable<GameBindingModel> {
    return this.http.put<GameBindingModel>(gamesUrl + id, body);
  }

  public deleteGame(id: string): Observable<GameBindingModel> {
    return this.http.delete<GameBindingModel>(gamesUrl + id);
  }
}
