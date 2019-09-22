import { IGamesState } from './games.state';
import * as GamesActions from './games.actions';

const initialState: IGamesState = {
  all: [],
  toEdit: null
};

function getAllGames(state: IGamesState, allGames: any) {
  return {
    ...state,
    all: allGames
  };
}

function getGameToEdit(state: IGamesState, gameToEdit: any) {
  return {
    ...state,
    toEdit: gameToEdit
  };
}

export function gamesReducer(state: IGamesState = initialState, action: GamesActions.Types) {
  switch (action.type) {
    case GamesActions.GET_ALL_GAMES:
      return getAllGames(state, action.payload);
    case GamesActions.GET_GAME_TO_EDIT:
      return getGameToEdit(state, action.payload);
    default:
      return state;
  }
}
