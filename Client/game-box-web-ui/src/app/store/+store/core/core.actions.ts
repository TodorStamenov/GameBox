import { Action } from '@ngrx/store';

import { IToastModel } from '../../../modules/core/models/toast.model';

export enum CoreActionTypes {
  DisplayToastMessage = '[CORE] Display Toast Message',
  HideToastMessage = '[CORE] Hide Toast Message'
}

export class DisplayToastMessage implements Action {
  readonly type = CoreActionTypes.DisplayToastMessage;
  constructor(public payload: IToastModel) { }
}

export class HideToastMessage implements Action {
  readonly type = CoreActionTypes.HideToastMessage;
}

export type Types = DisplayToastMessage
  | HideToastMessage;
