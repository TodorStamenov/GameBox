import { ICoreState } from './core.state';
import { CoreActionTypes, Types } from './core.actions';
import { IToastModel } from '../../../modules/core/models/toast.model';

const initialState: ICoreState = {
  toast: null
};

function displayToastMessage(state: ICoreState, toast: IToastModel): ICoreState {
  return {
    ...state,
    toast
  };
}

function hideToastMessage(state: ICoreState): ICoreState {
  return {
    ...state,
    toast: null
  };
}

export function coreReducer(state = initialState, action: Types): ICoreState {
  switch (action.type) {
    case CoreActionTypes.DisplayToastMessage:
      return displayToastMessage(state, action.payload);
    case CoreActionTypes.HideToastMessage:
      return hideToastMessage(state);
    default:
      return state;
  }
}
