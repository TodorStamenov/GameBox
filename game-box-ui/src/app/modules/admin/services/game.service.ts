import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { constants } from '../../../common';
import { GameBindingModel } from '../models/games/game-binding.model';

const gamesUrl = constants.host + 'games/';

@Injectable()
export class GameService {
  constructor(private http: HttpClient) { }

  getGame(id: string) {
    return this.http.get<GameBindingModel>(gamesUrl + id);
  }

  addGame(body: GameBindingModel) {
    return this.http.post<GameBindingModel>(gamesUrl, body);
  }

  editGame(id: string, body: GameBindingModel) {
    return this.http.put<GameBindingModel>(gamesUrl + id, body);
  }

  deleteGame(id: string) { 
    return this.http.delete<GameBindingModel>(gamesUrl + id);
  }
}