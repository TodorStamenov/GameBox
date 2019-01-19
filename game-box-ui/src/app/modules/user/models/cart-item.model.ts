export class CartItemModel {
  constructor(
    public id: string,
    public title: string,
    public description: string,
    public videoId: string,
    public thumbnailUrl: string,
    public price: number
  ) { }
}
