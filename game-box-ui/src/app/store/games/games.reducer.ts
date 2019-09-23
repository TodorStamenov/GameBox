import { IGamesState } from './games.state';
import * as GamesActions from './games.actions';

const initialState: IGamesState = {
  all: [],
  allHome: [],
  byCategory: [],
  owned: [],
  detail: null,
  toEdit: null
};

function getAllGames(state: IGamesState, allGames: any) {
  return {
    ...state,
    all: allGames
  };
}

function getAllGamesHome(state: IGamesState, allGamesHome: any) {
  return {
    ...state,
    allHome: [...state.allHome, ...allGamesHome]
  };
}

function getAllGamesByCategory(state: IGamesState, allGamesByCategory: any) {
  return {
    ...state,
    byCategory: [...state.byCategory, ...allGamesByCategory]
  };
}

function getAllOwnedGames(state: IGamesState, allOwnedGames: any) {
  return {
    ...state,
    owned: [...state.owned, ...allOwnedGames]
  };
}

function getGameDetail(state: IGamesState, gameDetail: any) {
  return {
    ...state,
    detail: gameDetail
  };
}

function getGameToEdit(state: IGamesState, gameToEdit: any) {
  return {
    ...state,
    toEdit: gameToEdit
  };
}

function clearGames(state: IGamesState, setToClear: string) {
  if (setToClear === 'home') {
    return {
      ...state,
      allHome: []
    };
  }

  if (setToClear === 'category') {
    return {
      ...state,
      byCategory: []
    };
  }

  if (setToClear === 'owned') {
    return {
      ...state,
      owned: []
    };
  }
}

export function gamesReducer(state: IGamesState = initialState, action: GamesActions.Types) {
  switch (action.type) {
    case GamesActions.GET_ALL_GAMES:
      return getAllGames(state, action.payload);
    case GamesActions.GET_ALL_GAMES_HOME:
      return getAllGamesHome(state, action.payload);
    case GamesActions.GET_ALL_GAMES_BY_CATEGORY:
      return getAllGamesByCategory(state, action.payload);
    case GamesActions.GET_ALL_OWNED_GAMES:
      return getAllOwnedGames(state, action.payload);
    case GamesActions.GET_GAME_DETAIL:
      return getGameDetail(state, action.payload);
    case GamesActions.GET_GAME_TO_EDIT:
      return getGameToEdit(state, action.payload);
    case GamesActions.CLEAR_GAMES:
      return clearGames(state, action.payload);
    default:
      return state;
  }
}
