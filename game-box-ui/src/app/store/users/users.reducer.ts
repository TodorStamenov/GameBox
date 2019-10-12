import { IUsersState } from './users.state';
import { UserActionTypes, Types } from './users.actions';
import { IUsersListModel } from 'src/app/modules/user/models/users-list.model';

const initialState: IUsersState = {
  all: []
};

function getAllUsers(state: IUsersState, allUsers: IUsersListModel[]): IUsersState {
  return {
    ...state,
    all: allUsers
  };
}

export function usersReducer(state = initialState, action: Types): IUsersState {
  switch (action.type) {
    case UserActionTypes.GetAllUsers:
      return getAllUsers(state, action.payload);

    default:
      return state;
  }
}
