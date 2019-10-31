import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';

import { mergeMap, map } from 'rxjs/operators';

import { GameService } from 'src/app/modules/game/services/game.service';
import {
  GameActionTypes,
  LoadAllGamesHome,
  GetAllGamesHome,
  LoadAllGamesByCategory,
  GetAllGamesByCategory,
  LoadAllOwnedGames,
  GetAllOwnedGames,
  LoadAllGames,
  GetAllGames,
  LoadGameToEdit,
  GetGameToEdit,
  LoadGameDetail,
  GetGameDetail
} from './games.actions';

@Injectable()
export class GamesEffects {
  constructor(
    private actions$: Actions,
    private gameService: GameService
  ) { }

  @Effect()
  loadGamesHome$ = this.actions$.pipe(
    ofType(GameActionTypes.LoadAllGamesHome),
    mergeMap((action: LoadAllGamesHome) => this.gameService.getGames$(action.payload).pipe(
      map(games => new GetAllGamesHome(games))
    ))
  );

  @Effect()
  loadGamesByCategory$ = this.actions$.pipe(
    ofType(GameActionTypes.LoadAllGamesByCategory),
    mergeMap((action: LoadAllGamesByCategory) =>
      this.gameService.getGamesByCategory$(action.payload.loadedGames, action.payload.categoryId).pipe(
        map(games => new GetAllGamesByCategory(games))
      ))
  );

  @Effect()
  loadOwnedGames$ = this.actions$.pipe(
    ofType(GameActionTypes.LoadAllOwnedGames),
    mergeMap((action: LoadAllOwnedGames) => this.gameService.getOwned$(action.payload).pipe(
      map(games => new GetAllOwnedGames(games))
    ))
  );

  @Effect()
  searchGames$ = this.actions$.pipe(
    ofType(GameActionTypes.LoadAllGames),
    mergeMap((action: LoadAllGames) => this.gameService.searchGames$(action.payload).pipe(
      map(games => new GetAllGames(games))
    ))
  );

  @Effect()
  getGameToEdit$ = this.actions$.pipe(
    ofType(GameActionTypes.LoadGameToEdit),
    mergeMap((action: LoadGameToEdit) => this.gameService.getGame$(action.payload).pipe(
      map(game => new GetGameToEdit(game))
    ))
  );

  @Effect()
  getGameDetails$ = this.actions$.pipe(
    ofType(GameActionTypes.LoadGameDetail),
    mergeMap((action: LoadGameDetail) => this.gameService.getDetails$(action.payload).pipe(
      map(game => new GetGameDetail(game))
    ))
  );
}
