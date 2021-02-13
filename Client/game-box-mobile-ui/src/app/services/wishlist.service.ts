import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';

import { IGameListItemModel } from '../modules/core/models/game-list-item.model';
import { UIService } from './ui.service';
import { constants } from '~/app/common';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  private wishlistUrl = constants.graphQlUrl;

  constructor(
    private http: HttpClient,
    private uiService: UIService
  ) { }

  public addItem$(id: string): Observable<void> {
    const mutation = `mutation addGameToWishlist($gameId: ID!) {
      addGameToWishlist(gameId: $gameId)
    }`;

    return this.http.post<void>(this.wishlistUrl, {
      operationName: 'addGameToWishlist',
      query: mutation,
      variables: { gameId: id }
    }).pipe(
      take(1)
    );
  }

  public removeItem$(id: string): Observable<void> {
    const mutation = `mutation removeGameFromWishlist ($gameId: ID!) {
      removeGameFromWishlist(gameId: $gameId)
    }`;

    return this.http.post<void>(this.wishlistUrl, {
      operationName: 'removeGameFromWishlist',
      query: mutation,
      variables: { gameId: id }
    }).pipe(
      take(1)
    );
  }

  public clearItems$(): Observable<void> {
    const mutation = `mutation clearGamesFromWishlist {
      clearGamesFromWishlist
    }`;

    return this.http.post<void>(this.wishlistUrl, {
      operationName: 'clearGamesFromWishlist',
      query: mutation,
      variables: {}
    }).pipe(
      take(1)
    );
  }

  public getItems$(): Observable<IGameListItemModel[]> {
    const query = `
      query wishlist {
        wishlist {
          id
          title
          price
          thumbnailUrl
          videoId
        }
      }`;

    return this.http.post<any>(this.wishlistUrl, {
      operationName: 'wishlist',
      query,
      variables: {}
    }).pipe(
      take(1),
      map(res => res.data.wishlist),
      map((games: IGameListItemModel[]) => games.map(game => {
        game.thumbnailUrl = this.uiService.changeThumbnailUrls(game.thumbnailUrl, game.videoId);
        return game;
      }))
    );
  }
}
