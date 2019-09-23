import { Action } from '@ngrx/store';
import { IUsersListModel } from 'src/app/modules/user/models/users-list.model';

export const GET_ALL_USERS = '[USERS] Get All';

export class GetAllUsers implements Action {
  type = GET_ALL_USERS;
  constructor(public payload: IUsersListModel[]) { }
}

export type Types = GetAllUsers;
