import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Store } from '@ngrx/store';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { constants } from 'src/app/common';
import { IGamesHomeListModel } from '../models/games/games-list.model';
import { IGameDetailsModel } from '../models/games/game-details.model';
import { IAppState } from 'src/app/store/app.state';
import { GetAllGamesByCategory, GetAllOwnedGames, GetGameDetail } from 'src/app/store/games/games.actions';

const gamesUrl = constants.host + 'games/';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  constructor(
    private http: HttpClient,
    private store: Store<IAppState>
  ) { }

  public getGames$(loadedGames: number, categoryId: string): Observable<void> {
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
}
