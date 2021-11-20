import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';

import { mergeMap, map } from 'rxjs/operators';

import { GameService } from 'src/app/services/game.service';
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
  LoadGameById,
  GetGameById,
  LoadGameDetails,
  GetGameDetails,
  LoadGameComments,
  GetGameComments
} from './games.actions';

@Injectable()
export class GamesEffects {
  constructor(
    private actions$: Actions,
    private gameService: GameService
  ) { }

  loadGamesHome$ = createEffect(() => this.actions$.pipe(
    ofType(GameActionTypes.LoadAllGamesHome),
    mergeMap((action: LoadAllGamesHome) => this.gameService.getGames$(action.payload).pipe(
      map(games => new GetAllGamesHome(games))
    ))
  ));

  loadGamesByCategory$ = createEffect(() => this.actions$.pipe(
    ofType(GameActionTypes.LoadAllGamesByCategory),
    mergeMap((action: LoadAllGamesByCategory) =>
      this.gameService.getGamesByCategory$(action.payload.loadedGames, action.payload.categoryId).pipe(
        map(games => new GetAllGamesByCategory(games))
      ))
  ));

  loadOwnedGames$ = createEffect(() => this.actions$.pipe(
    ofType(GameActionTypes.LoadAllOwnedGames),
    mergeMap((action: LoadAllOwnedGames) => this.gameService.getOwned$(action.payload).pipe(
      map(games => new GetAllOwnedGames(games))
    ))
  ));

  searchGames$ = createEffect(() => this.actions$.pipe(
    ofType(GameActionTypes.LoadAllGames),
    mergeMap((action: LoadAllGames) => this.gameService.searchGames$(action.payload).pipe(
      map(games => new GetAllGames(games))
    ))
  ));

  getGameToEdit$ = createEffect(() => this.actions$.pipe(
    ofType(GameActionTypes.LoadGameById),
    mergeMap((action: LoadGameById) => this.gameService.getGame$(action.payload).pipe(
      map(game => new GetGameById(game))
    ))
  ));

  getGameDetails$ = createEffect(() => this.actions$.pipe(
    ofType(GameActionTypes.LoadGameDetails),
    mergeMap((action: LoadGameDetails) => this.gameService.getDetails$(action.payload).pipe(
      map(game => new GetGameDetails(game))
    ))
  ));

  getGameComments$ = createEffect(() => this.actions$.pipe(
    ofType(GameActionTypes.LoadGameComments),
    mergeMap((action: LoadGameComments) => this.gameService.getComments$(action.payload).pipe(
      map(games => new GetGameComments(games))
    ))
  ));
}
