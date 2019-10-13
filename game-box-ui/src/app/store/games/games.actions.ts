import { Action } from '@ngrx/store';

import { IGamesListModel } from 'src/app/modules/game/models/games-list.model';
import { IGameBindingModel } from 'src/app/modules/game/models/game-binding.model';
import { IGamesHomeListModel } from 'src/app/modules/game/models/games-home-list.model';
import { IGameDetailsModel } from 'src/app/modules/game/models/game-details.model';

export enum GameActionTypes {
  LoadAllGames = '[GAMES] Load All Games',
  GetAllGames = '[GAMES] Get All Games',
  LoadAllGamesHome = '[GAMES] Load All Games Home',
  GetAllGamesHome = '[GAMES] Get All Games Home',
  LoadAllGamesByCategory = '[GAMES] Load All Games By Category',
  GetAllGamesByCategory = '[GAMES] Get All Games By Category',
  LoadAllOwnedGames = '[GAMES] Load All Owned Games',
  GetAllOwnedGames = '[GAMES] Get All Owned Games',
  LoadGameDetail = '[GAMES] Load Detail',
  GetGameDetail = '[GAMES] Get Detail',
  LoadGameToEdit = '[GAMES] Load To Edit',
  GetGameToEdit = '[GAMES] Get To Edit',
  ClearGamesHome = '[GAMES] Clear Games Home',
  ClearGamesByCategory = '[GAMES] Clear Games By Category',
  ClearOwnedGames = '[GAMES] Clear Owned Games',
}

export class LoadAllGames implements Action {
  readonly type = GameActionTypes.LoadAllGames;
  constructor(public payload: string) { }
}

export class GetAllGames implements Action {
  readonly type = GameActionTypes.GetAllGames;
  constructor(public payload: IGamesListModel[]) { }
}

export class LoadAllGamesHome implements Action {
  readonly type = GameActionTypes.LoadAllGamesHome;
  constructor(public payload: number) { }
}

export class GetAllGamesHome implements Action {
  readonly type = GameActionTypes.GetAllGamesHome;
  constructor(public payload: IGamesHomeListModel[]) { }
}

export class LoadAllGamesByCategory implements Action {
  readonly type = GameActionTypes.LoadAllGamesByCategory;
  constructor(public payload: { loadedGames: number, categoryId: string }) { }
}

export class GetAllGamesByCategory implements Action {
  readonly type = GameActionTypes.GetAllGamesByCategory;
  constructor(public payload: IGamesHomeListModel[]) { }
}

export class LoadAllOwnedGames implements Action {
  readonly type = GameActionTypes.LoadAllOwnedGames;
  constructor(public payload: number) { }
}

export class GetAllOwnedGames implements Action {
  readonly type = GameActionTypes.GetAllOwnedGames;
  constructor(public payload: IGamesHomeListModel[]) { }
}

export class LoadGameDetail implements Action {
  readonly type = GameActionTypes.LoadGameDetail;
  constructor(public payload: string) { }
}

export class GetGameDetail implements Action {
  readonly type = GameActionTypes.GetGameDetail;
  constructor(public payload: IGameDetailsModel) { }
}

export class LoadGameToEdit implements Action {
  readonly type = GameActionTypes.LoadGameToEdit;
  constructor(public payload: string) { }
}

export class GetGameToEdit implements Action {
  readonly type = GameActionTypes.GetGameToEdit;
  constructor(public payload: IGameBindingModel) { }
}

export class ClearGamesHome implements Action {
  readonly type = GameActionTypes.ClearGamesHome;
}

export class ClearGamesByCategory implements Action {
  readonly type = GameActionTypes.ClearGamesByCategory;
}

export class ClearOwnedGames implements Action {
  readonly type = GameActionTypes.ClearOwnedGames;
}

export type Types = LoadAllGames
  | GetAllGames
  | LoadAllGamesHome
  | GetAllGamesHome
  | LoadAllGamesByCategory
  | GetAllGamesByCategory
  | LoadAllOwnedGames
  | GetAllOwnedGames
  | LoadGameDetail
  | GetGameDetail
  | LoadGameToEdit
  | GetGameToEdit
  | ClearGamesHome
  | ClearGamesByCategory
  | ClearOwnedGames;
