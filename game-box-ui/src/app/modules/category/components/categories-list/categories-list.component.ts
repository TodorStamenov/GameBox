import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';

import { IAppState } from 'src/app/store/app.state';
import { ICategoriesListModel } from '../../models/categories-list.model';
import { LoadAllCategories } from 'src/app/store/categories/categories.actions';

@Component({
  selector: 'app-categories-list',
  templateUrl: './categories-list.component.html'
})
export class CategoriesListComponent implements OnInit {
  public categories$: Observable<ICategoriesListModel[]>;

  constructor(private store: Store<IAppState>) { }

  public ngOnInit() {
    this.store.dispatch(new LoadAllCategories());
    this.categories$ = this.store.pipe(
      select(s => s.categories.all)
    );
  }
}
