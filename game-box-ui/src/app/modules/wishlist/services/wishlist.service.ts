import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Apollo } from 'apollo-angular';
import gql from 'graphql-tag';

import { constants } from 'src/app/common';
import { IGameListItemModel } from '../../core/models/game-list-item.model';
import { ApolloQueryResult } from 'apollo-client';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  constructor(
    private http: HttpClient,
    private apollo: Apollo
  ) { }

  public addItem$(id: string): Observable<void> {
    const query = { };

    return this.queryGraphQlEndpoint<void>(query);
  }

  public removeItem$(id: string): Observable<void> {
    const query = { };

    return this.queryGraphQlEndpoint<void>(query);
  }

  public clearItems$(): Observable<void> {
    const query = { };

    return this.queryGraphQlEndpoint<void>(query);
  }

  public getItems$(): Observable<IGameListItemModel[]> {
    const query = {
      query: gql`{ wishlist { id title price description thumbnailUrl videoId } }`
    };

    return this.apollo
      .watchQuery(query)
      .valueChanges
      .pipe(
        map(({ data }: any) => data.wishlist)
      );
  }

  private queryGraphQlEndpoint<T>(query: any): Observable<T> {
    return this.http.post<T>(constants.graphQl, query);
  }
}
