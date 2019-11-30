import { IUsersListModel } from 'src/app/modules/user/models/users-list.model';
import { IAppState } from 'src/app/store/app.state';

export interface IState extends IAppState {
  users: IUsersState;
}

export interface IUsersState {
  all: IUsersListModel[];
}
