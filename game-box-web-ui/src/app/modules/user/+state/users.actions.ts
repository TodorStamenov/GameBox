import { Action } from '@ngrx/store';

import { IUsersListModel } from 'src/app/modules/user/models/users-list.model';

export enum UserActionTypes {
  LoadAllUsers = '[USERS] Load All Users',
  GetAllUsers = '[USERS] Get All Users'
}

export class LoadAllUsers implements Action {
  readonly type = UserActionTypes.LoadAllUsers;
  constructor(public payload: string) { }
}

export class GetAllUsers implements Action {
  readonly type = UserActionTypes.GetAllUsers;
  constructor(public payload: IUsersListModel[]) { }
}

export type Types = LoadAllUsers
  | GetAllUsers;
