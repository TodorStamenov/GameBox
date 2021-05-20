import { IGameOrderModel } from './game-order.model';

export interface IOrdersListModel {
  username: string;
  timeStamp: Date;
  price: number;
  gamesCount: number;
  games: IGameOrderModel[];
}
