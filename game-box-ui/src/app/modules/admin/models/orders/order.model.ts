export class OrderModel {
  constructor(
    public username: string,
    public timeStamp: Date,
    public price: number,
    public gamesCount: number
  ) { }
}
