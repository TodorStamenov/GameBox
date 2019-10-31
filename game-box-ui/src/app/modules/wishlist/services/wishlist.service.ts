import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { constants } from 'src/app/common';
import { IGameListItemModel } from '../../core/models/game-list-item.model';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  constructor(private http: HttpClient) { }

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
      query: '{ wishlist { id title price description thumbnailUrl videoId } }'
    };

    return this.queryGraphQlEndpoint<any>(query).pipe(
      map(res => res.data.wishlist)
    );
  }

  private queryGraphQlEndpoint<T>(query: any): Observable<T> {
    return this.http.post<T>(constants.graphQl, query);
  }
}
