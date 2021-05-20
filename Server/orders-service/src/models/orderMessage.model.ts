export interface OrderMessageModel {
  username: string;
  price: number;
  gamesCount: number;
  timeStamp: string;
  games: OrderGameModel[];
}

export interface OrderGameModel {
  id: string;
  title: string;
  viewCount: number;
  price: number;
  orderCount: number;
}
