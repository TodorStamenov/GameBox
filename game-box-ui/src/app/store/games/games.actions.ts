import { Action } from '@ngrx/store';

import { IGamesListModel } from 'src/app/modules/game/models/games-list.model';
import { IGameBindingModel } from 'src/app/modules/game/models/game-binding.model';
import { IGamesHomeListModel } from 'src/app/modules/game/models/games-home-list.model';
import { IGameDetailsModel } from 'src/app/modules/game/models/game-details.model';

export const GET_ALL_GAMES = '[GAMES] Get All';
export const GET_ALL_GAMES_HOME = '[GAMES] Get All Home';
export const GET_ALL_GAMES_BY_CATEGORY = '[GAMES] Get All Games By Category';
export const GET_ALL_OWNED_GAMES = '[GAMES] Get All Owned Games';
export const GET_GAME_DETAIL = '[GAMES] Get Detail';
export const GET_GAME_TO_EDIT = '[GAMES] Get To Edit';
export const CLEAR_GAMES = '[GAMES] Clear Games';

export class GetAllGames implements Action {
  readonly type = GET_ALL_GAMES;
  constructor(public payload: IGamesListModel[]) { }
}

export class GetAllGamesHome implements Action {
  readonly type = GET_ALL_GAMES_HOME;
  constructor(public payload: IGamesHomeListModel[]) { }
}

export class GetAllGamesByCategory implements Action {
  readonly type = GET_ALL_GAMES_BY_CATEGORY;
  constructor(public payload: IGamesHomeListModel[]) { }
}

export class GetAllOwnedGames implements Action {
  readonly type = GET_ALL_OWNED_GAMES;
  constructor(public payload: IGamesHomeListModel[]) { }
}

export class GetGameDetail implements Action {
  readonly type = GET_GAME_DETAIL;
  constructor(public payload: IGameDetailsModel) { }
}

export class GetGameToEdit implements Action {
  readonly type = GET_GAME_TO_EDIT;
  constructor(public payload: IGameBindingModel) { }
}

export class ClearGames implements Action {
  readonly type = CLEAR_GAMES;
  constructor(public payload: string) { }
}

export type Types = GetAllGames
  | GetAllGamesHome
  | GetAllGamesByCategory
  | GetAllOwnedGames
  | GetGameDetail
  | GetGameToEdit
  | ClearGames;
