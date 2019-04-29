import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { constants } from '../../../common';
import { IGameBindingModel } from '../models/games/game-binding.model';
import { Observable } from 'rxjs';

const gamesUrl = constants.host + 'games/';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  constructor(private http: HttpClient) { }

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
