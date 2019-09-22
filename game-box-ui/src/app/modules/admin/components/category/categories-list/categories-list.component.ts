import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';

import { AppState } from 'src/app/store/app.state';
import { CategoryService } from '../../../services/category.service';
import { ICategoriesListModel } from '../../../models/categories/categories-list.model';

@Component({
  selector: 'app-categories-list',
  templateUrl: './categories-list.component.html'
})
export class CategoriesListComponent implements OnInit {
  public categories$: Observable<ICategoriesListModel[]>;

  constructor(
    private categoryService: CategoryService,
    private store: Store<AppState>
  ) { }

  public ngOnInit() {
    this.categoryService
      .getCategories$()
      .subscribe(() => {
        this.categories$ = this.store.pipe(
          select(state => state.categories.all)
        );
      });
  }
}
