import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Store } from '@ngrx/store';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { constants } from '../../../common';
import { IGameBindingModel } from '../models/games/game-binding.model';
import { IGamesListModel } from '../models/games/games-list.model';
import { IAppState } from 'src/app/store/app.state';
import { GetAllGames, GetGameToEdit } from 'src/app/store/games/games.actions';

const gamesUrl = constants.host + 'games/';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  constructor(
    private http: HttpClient,
    private store: Store<IAppState>
  ) { }

  public getGames$(title: string = ''): Observable<void> {
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
