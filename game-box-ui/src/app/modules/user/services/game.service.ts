import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ListGamesModel } from "../models/list-games.model";
import { constants } from "src/app/common";
import { GameDetailsModel } from "../models/game-details.model";

const gamesUrl = constants.host + 'games/';

@Injectable()
export class GameService {
  constructor(private http: HttpClient) { }

  getGames(loadedGames: number, categoryId: string) {
    return this.http.get<ListGamesModel[]>(gamesUrl + '?loadedGames=' + loadedGames + '&categoryId=' + categoryId);
  }

  getOwned(loadedGames: number) {
    return this.http.get<ListGamesModel[]>(gamesUrl + 'owned?loadedGames=' + loadedGames);
  }

  getDetails(id: string) {
    return this.http.get<GameDetailsModel>(gamesUrl + 'details/' + id);
  }
}