import { IUsersState } from './users.state';
import * as UsersActions from './users.actions';

const initialState: IUsersState = {
  all: []
};

function getAllUsers(state: IUsersState, allUsers: any): IUsersState {
  return {
    ...state,
    all: allUsers
  };
}

export function usersReducer(
  state: IUsersState = initialState,
  action: UsersActions.Types): IUsersState {

  switch (action.type) {
    case UsersActions.GET_ALL_USERS:
      return getAllUsers(state, action.payload);

    default:
      return state;
  }
}
