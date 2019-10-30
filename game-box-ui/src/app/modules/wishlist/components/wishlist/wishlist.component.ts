import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';

import { IAppState } from 'src/app/store/app.state';
import { IGameListItemModel } from 'src/app/modules/core/models/game-list-item.model';
import { LoadAllItems, ClearItems, RemoveItem } from 'src/app/store/wishlist/wishlist.actions';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.scss']
})
export class WishlistComponent implements OnInit, OnDestroy {
  public totalPrice: number;
  public games$: Observable<IGameListItemModel[]>;

  constructor(private store: Store<IAppState>) { }

  public ngOnInit(): void {
    this.store.dispatch(new LoadAllItems());
    this.games$ = this.store.pipe(
      select(s => s.wishlist.all)
    );
  }

  public ngOnDestroy(): void {
    this.onClearItems();
  }

  public onClearItems(): void {
    this.store.dispatch(new ClearItems());
  }

  public onRemoveItem(id: string): void {
    this.store.dispatch(new RemoveItem(id));
  }
}
