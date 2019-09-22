import { Action } from '@ngrx/store';

import { IGamesListModel } from 'src/app/modules/admin/models/games/games-list.model';
import { IGameBindingModel } from 'src/app/modules/admin/models/games/game-binding.model';
import { IGamesHomeListModel } from 'src/app/modules/user/models/games/games-list.model';
import { IGameDetailsModel } from 'src/app/modules/user/models/games/game-details.model';

export const GET_ALL_GAMES = '[GAMES] Get All';
export const GET_ALL_GAMES_BY_CATEGORY = '[GAMES] Get All Games By Category';
export const GET_ALL_OWNED_GAMES = '[GAMES] Get All Owned Games';
export const GET_GAME_DETAIL = '[GAMES] Get Detail';
export const GET_GAME_TO_EDIT = '[GAMES] Get To Edit';

export class GetAllGames implements Action {
  type = GET_ALL_GAMES;
  constructor(public payload: IGamesListModel[]) { }
}

export class GetAllGamesByCategory implements Action {
  type = GET_ALL_GAMES_BY_CATEGORY;
  constructor(public payload: IGamesHomeListModel[]) { }
}

export class GetAllOwnedGames implements Action {
  type = GET_ALL_OWNED_GAMES;
  constructor(public payload: IGamesHomeListModel[]) { }
}

export class GetGameDetail implements Action {
  type = GET_GAME_DETAIL;
  constructor(public payload: IGameDetailsModel) { }
}

export class GetGameToEdit implements Action {
  type = GET_GAME_TO_EDIT;
  constructor(public payload: IGameBindingModel) { }
}

export type Types = GetAllGames | GetAllGamesByCategory | GetAllOwnedGames | GetGameDetail | GetGameToEdit;
