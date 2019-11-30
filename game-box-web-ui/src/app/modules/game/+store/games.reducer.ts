import { IGamesState } from './games.state';
import { GameActionTypes, Types } from './games.actions';
import { IGamesListModel } from 'src/app/modules/game/models/games-list.model';
import { IGamesHomeListModel } from 'src/app/modules/game/models/games-home-list.model';
import { IGameDetailsModel } from 'src/app/modules/game/models/game-details.model';
import { IGameBindingModel } from 'src/app/modules/game/models/game-binding.model';

const initialState: IGamesState = {
  all: [],
  allHome: [],
  byCategory: [],
  owned: [],
  details: null,
  byId: null
};

function getAllGames(state: IGamesState, allGames: IGamesListModel[]): IGamesState {
  return {
    ...state,
    all: allGames
  };
}

function getAllGamesHome(state: IGamesState, allGamesHome: IGamesHomeListModel[]): IGamesState {
  return {
    ...state,
    allHome: [...state.allHome, ...allGamesHome]
  };
}

function getAllGamesByCategory(state: IGamesState, allGamesByCategory: IGamesHomeListModel[]): IGamesState {
  return {
    ...state,
    byCategory: [...state.byCategory, ...allGamesByCategory]
  };
}

function getAllOwnedGames(state: IGamesState, allOwnedGames: IGamesHomeListModel[]): IGamesState {
  return {
    ...state,
    owned: [...state.owned, ...allOwnedGames]
  };
}

function getGameDetail(state: IGamesState, gameDetail: IGameDetailsModel): IGamesState {
  return {
    ...state,
    details: gameDetail
  };
}

function getGameToEdit(state: IGamesState, gameToEdit: IGameBindingModel): IGamesState {
  return {
    ...state,
    byId: gameToEdit
  };
}

function clearGamesHome(state: IGamesState): IGamesState {
  return {
    ...state,
    allHome: []
  };
}

function clearGamesByCategory(state: IGamesState): IGamesState {
  return {
    ...state,
    byCategory: []
  };
}

function clearOwnedGames(state: IGamesState): IGamesState {
  return {
    ...state,
    owned: []
  };
}

export function gamesReducer(state = initialState, action: Types): IGamesState {
  switch (action.type) {
    case GameActionTypes.GetAllGames:
      return getAllGames(state, action.payload);

    case GameActionTypes.GetAllGamesHome:
      return getAllGamesHome(state, action.payload);

    case GameActionTypes.GetAllGamesByCategory:
      return getAllGamesByCategory(state, action.payload);

    case GameActionTypes.GetAllOwnedGames:
      return getAllOwnedGames(state, action.payload);

    case GameActionTypes.GetGameDetails:
      return getGameDetail(state, action.payload);

    case GameActionTypes.GetGameById:
      return getGameToEdit(state, action.payload);

    case GameActionTypes.ClearGamesHome:
      return clearGamesHome(state);

    case GameActionTypes.ClearGamesByCategory:
      return clearGamesByCategory(state);

    case GameActionTypes.ClearOwnedGames:
      return clearOwnedGames(state);

    default:
      return state;
  }
}
