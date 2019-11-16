import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { constants } from '~/app/common';
import { IGameListItemModel } from '../../core/models/game-list-item.model';
import { UIService } from '../../core/services/ui.service';

const wishlistUrl = constants.graphQl;

@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  constructor(
    private http: HttpClient,
    private uiService: UIService
  ) { }

  public addItem$(id: string): Observable<void> {
    const mutation = `mutation addGameToWishlist($gameId: ID!) {
      addGameToWishlist(gameId: $gameId)
    }`;

    return this.http.post<void>(wishlistUrl, {
      query: mutation,
      variables: { gameId: id }
    });
  }

  public removeItem$(id: string): Observable<void> {
    const mutation = `mutation removeGameFromWishlist ($gameId: ID!) {
      removeGameFromWishlist(gameId: $gameId)
    }`;

    console.log(id);

    return this.http.post<void>(wishlistUrl, {
      query: mutation,
      variables: { gameId: id }
    });
  }

  public clearItems$(): Observable<void> {
    const mutation = `mutation clearGamesFromWishlist { clearGamesFromWishlist }`;

    return this.http.post<void>(wishlistUrl, { query: mutation });
  }

  public getItems$(): Observable<IGameListItemModel[]> {
    const query = `{
      wishlist {
        id
        title
        price
        thumbnailUrl
        videoId
      }
    }`;

    return this.http.post<any>(wishlistUrl, { query }).pipe(
      map(res => res.data.wishlist),
      map((games: IGameListItemModel[]) => games.map(game => {
        game.thumbnailUrl = this.uiService.changeThumbnailUrls(game.thumbnailUrl, game.videoId);
        return game;
      }))
    );
  }
}
