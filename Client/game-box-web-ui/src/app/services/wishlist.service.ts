import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Apollo, gql } from 'apollo-angular';

import { IGameListItemModel } from '../modules/core/models/game-list-item.model';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  private wishlistQuery = gql`
    query wishlist {
      wishlist {
        id
        title
        price
        description
        thumbnailUrl
        videoId
      }
    }`;

  constructor(private apollo: Apollo) { }

  public addItem$(id: string): Observable<any> {
    const mutation = gql`mutation addGame($input: AddGameInput) {
      addGame(input: $input)
    }`;

    return this.apollo.mutate({
      mutation,
      variables: {
        input: {
          gameId: id
        }
      },
      refetchQueries: [{
        query: this.wishlistQuery
      }]
    });
  }

  public removeItem$(id: string): Observable<any> {
    const mutation = gql`mutation removeGame($input: RemoveGameInput) {
      removeGame(input: $input)
    }`;

    return this.apollo.mutate({
      mutation,
      variables: {
        input: {
          gameId: id
        }
      },
      refetchQueries: [{
        query: this.wishlistQuery
      }]
    });
  }

  public clearItems$(): Observable<any> {
    const mutation = gql`mutation clearGames {
      clearGames
    }`;

    return this.apollo.mutate({
      mutation,
      refetchQueries: [{
        query: this.wishlistQuery
      }]
    });
  }

  public getItems$(): Observable<IGameListItemModel[]> {
    return this.apollo
      .watchQuery({ query: this.wishlistQuery })
      .valueChanges
      .pipe(
        map(({ data }: any) => data.wishlist)
      );
  }
}
