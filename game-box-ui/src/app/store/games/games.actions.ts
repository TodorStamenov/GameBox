import { Action } from '@ngrx/store';

import { IGamesListModel } from 'src/app/modules/admin/models/games/games-list.model';
import { IGameBindingModel } from 'src/app/modules/admin/models/games/game-binding.model';

export const GET_ALL_GAMES = '[GAMES] Get All';
export const GET_GAME_TO_EDIT = '[GAMES] Get To Edit';

export class GetAllGames implements Action {
  type = GET_ALL_GAMES;
  constructor(public payload: IGamesListModel[]) { }
}

export class GetGameToEdit implements Action {
  type = GET_GAME_TO_EDIT;
  constructor(public payload: IGameBindingModel) { }
}

export type Types = GetAllGames | GetGameToEdit;
