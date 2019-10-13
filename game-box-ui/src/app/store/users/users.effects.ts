import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';

import { mergeMap, map } from 'rxjs/operators';

import { UserService } from 'src/app/modules/user/services/user.service';
import { UserActionTypes, LoadAllUsers, GetAllUsers } from './users.actions';

@Injectable()
export class UsersEffects {
  constructor(
    private actions$: Actions,
    private userService: UserService
  ) { }

  @Effect()
  loadUsers$ = this.actions$.pipe(
    ofType(UserActionTypes.LoadAllUsers),
    mergeMap((action: LoadAllUsers) => this.userService.getUsers$(action.payload).pipe(
      map(users => new GetAllUsers(users))
    ))
  );
}
