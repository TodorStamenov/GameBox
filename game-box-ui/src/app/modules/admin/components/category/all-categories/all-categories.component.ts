import { Component, OnInit } from '@angular/core';

import { Observable } from 'rxjs';

import { ListCategoriesModel } from '../../../models/categories/list-categories.model';
import { CategoryService } from '../../../services/category.service';

@Component({
  selector: 'app-all-categories',
  templateUrl: './all-categories.component.html'
})
export class AllCategoriesComponent implements OnInit {
  public categories$: Observable<ListCategoriesModel[]>;

  constructor(private categoryService: CategoryService) { }

  public ngOnInit(): void {
    this.categories$ = this.categoryService.getCategories();
  }
}
