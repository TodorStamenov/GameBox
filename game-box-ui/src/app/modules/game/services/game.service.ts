import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Store } from '@ngrx/store';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { constants } from '../../../common';
import { IAppState } from 'src/app/store/app.state';
import { IGameBindingModel } from '../models/game-binding.model';
import { IGamesListModel } from '../models/games-list.model';
import { IGamesHomeListModel } from '../models/games-home-list.model';
import { IGameDetailsModel } from '../models/game-details.model';
import {
  GetAllGames,
  GetGameToEdit,
  GetAllGamesByCategory,
  GetAllOwnedGames,
  GetGameDetail,
  GetAllGamesHome
} from 'src/app/store/games/games.actions';

const gamesUrl = constants.host + 'games/';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  constructor(
    private http: HttpClient,
    private store: Store<IAppState>
  ) { }

  public searchGames$(title: string = ''): Observable<void> {
    return this.http.get<IGamesListModel[]>(gamesUrl + 'all?title=' + title).pipe(
      map((games: IGamesListModel[]) => {
        this.store.dispatch(new GetAllGames(games));
      })
    );
  }

  public getGame$(id: string): Observable<void> {
    return this.http.get<IGameBindingModel>(gamesUrl + id).pipe(
      map((game: IGameBindingModel) => {
        this.store.dispatch(new GetGameToEdit(game));
      })
    );
  }

  public getGames$(loadedGames: number): Observable<void> {
    return this.http.get<IGamesHomeListModel[]>(gamesUrl + '?loadedGames=' + loadedGames).pipe(
      map((games: IGamesHomeListModel[]) => {
        this.store.dispatch(new GetAllGamesHome(games));
      })
    );
  }

  public getGamesByCategory$(loadedGames: number, categoryId: string): Observable<void> {
    return this.http.get<IGamesHomeListModel[]>(gamesUrl + '?loadedGames=' + loadedGames + '&categoryId=' + categoryId).pipe(
      map((games: IGamesHomeListModel[]) => {
        this.store.dispatch(new GetAllGamesByCategory(games));
      })
    );
  }

  public getOwned$(loadedGames: number): Observable<void> {
    return this.http.get<IGamesHomeListModel[]>(gamesUrl + 'owned?loadedGames=' + loadedGames).pipe(
      map((games: IGamesHomeListModel[]) => {
        this.store.dispatch(new GetAllOwnedGames(games));
      })
    );
  }

  public getDetails$(id: string): Observable<void> {
    return this.http.get<IGameDetailsModel>(gamesUrl + 'details/' + id).pipe(
      map((game: IGameDetailsModel) => {
        this.store.dispatch(new GetGameDetail(game));
      })
    );
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
