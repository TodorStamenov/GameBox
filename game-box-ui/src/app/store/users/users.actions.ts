import { Action } from '@ngrx/store';

import { IUsersListModel } from 'src/app/modules/user/models/users-list.model';

export enum UserActionTypes {
  GetAllUsers = '[USERS] Get All'
}

export class GetAllUsers implements Action {
  readonly type = UserActionTypes.GetAllUsers;
  constructor(public payload: IUsersListModel[]) { }
}

export type Types = GetAllUsers;
