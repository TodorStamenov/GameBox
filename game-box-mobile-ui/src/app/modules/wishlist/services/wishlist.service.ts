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
    const mutation = `mutation addGameToWishlist($gameId: ID!, $userId: ID!) {
      addGameToWishlist(gameId: $gameId, userId: $userId)
    }`;

    return this.http.post<void>(wishlistUrl, {
      operationName: 'addGameToWishlist',
      query: mutation,
      variables: { gameId: id }
    });
  }

  public removeItem$(id: string): Observable<void> {
    const mutation = `mutation removeGameFromWishlist ($gameId: ID!, $userId: ID!) {
      removeGameFromWishlist(gameId: $gameId, userId: $userId)
    }`;

    return this.http.post<void>(wishlistUrl, {
      operationName: 'removeGameFromWishlist',
      query: mutation,
      variables: { gameId: id }
    });
  }

  public clearItems$(): Observable<void> {
    const mutation = `mutation clearGamesFromWishlist($userId: ID!) {
      clearGamesFromWishlist(userId: $userId)
    }`;

    return this.http.post<void>(wishlistUrl, {
      operationName: 'clearGamesFromWishlist',
      query: mutation,
      variables: {}
    });
  }

  public getItems$(): Observable<IGameListItemModel[]> {
    const query = `
      query wishlist($userId: ID!) {
        wishlist(userId: $userId) {
          id
          title
          price
          thumbnailUrl
          videoId
        }
      }`;

    return this.http.post<any>(wishlistUrl, {
      operationName: 'wishlist',
      query,
      variables: {}
    }).pipe(
      map(res => res.data.wishlist),
      map((games: IGameListItemModel[]) => games.map(game => {
        game.thumbnailUrl = this.uiService.changeThumbnailUrls(game.thumbnailUrl, game.videoId);
        return game;
      }))
    );
  }
}
