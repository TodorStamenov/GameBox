export class GameBindingModel {
  constructor(
    public title: string,
    public description: string,
    public price: number,
    public size: number,
    public thumbnailUrl: string,
    public videoId: string,
    public releaseDate: Date,
    public categoryId: string
  ) { }
}