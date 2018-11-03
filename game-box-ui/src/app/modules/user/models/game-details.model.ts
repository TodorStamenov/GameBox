export class GameDetailsModel {
  constructor(
    public id: string,
    public title: string,
    public price: number,
    public size: number,
    public videoId: string,
    public thumbnailUrl: string,
    public description: string,
    public viewCount: number,
    public releaseDate: Date
  ) { }
}