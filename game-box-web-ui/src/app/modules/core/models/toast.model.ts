import { ToastType } from '../enums/toast-type.enum';

export interface IToastModel {
  message: string;
  toastType: ToastType;
}
