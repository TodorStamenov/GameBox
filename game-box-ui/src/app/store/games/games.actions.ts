import { Action } from '@ngrx/store';

import { IGamesListModel } from 'src/app/modules/game/models/games-list.model';
import { IGameBindingModel } from 'src/app/modules/game/models/game-binding.model';
import { IGamesHomeListModel } from 'src/app/modules/game/models/games-home-list.model';
import { IGameDetailsModel } from 'src/app/modules/game/models/game-details.model';

export enum GameActionTypes {
  GetAllGames = '[GAMES] Get All',
  GetAllGamesHome = '[GAMES] Get All Home',
  GetAllGamesByCategory = '[GAMES] Get All Games By Category',
  GetAllOwnedGames = '[GAMES] Get All Owned Games',
  GetGameDetail = '[GAMES] Get Detail',
  GetGameToEdit = '[GAMES] Get To Edit',
  ClearGames = '[GAMES] Clear Games'
}

export class GetAllGames implements Action {
  readonly type = GameActionTypes.GetAllGames;
  constructor(public payload: IGamesListModel[]) { }
}

export class GetAllGamesHome implements Action {
  readonly type = GameActionTypes.GetAllGamesHome;
  constructor(public payload: IGamesHomeListModel[]) { }
}

export class GetAllGamesByCategory implements Action {
  readonly type = GameActionTypes.GetAllGamesByCategory;
  constructor(public payload: IGamesHomeListModel[]) { }
}

export class GetAllOwnedGames implements Action {
  readonly type = GameActionTypes.GetAllOwnedGames;
  constructor(public payload: IGamesHomeListModel[]) { }
}

export class GetGameDetail implements Action {
  readonly type = GameActionTypes.GetGameDetail;
  constructor(public payload: IGameDetailsModel) { }
}

export class GetGameToEdit implements Action {
  readonly type = GameActionTypes.GetGameToEdit;
  constructor(public payload: IGameBindingModel) { }
}

export class ClearGames implements Action {
  readonly type = GameActionTypes.ClearGames;
  constructor(public payload: string) { }
}

export type Types = GetAllGames
  | GetAllGamesHome
  | GetAllGamesByCategory
  | GetAllOwnedGames
  | GetGameDetail
  | GetGameToEdit
  | ClearGames;
