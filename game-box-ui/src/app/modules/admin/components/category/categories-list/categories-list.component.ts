import { Component } from '@angular/core';

import { CategoryService } from '../../../services/category.service';

@Component({
  selector: 'app-categories-list',
  templateUrl: './categories-list.component.html'
})
export class CategoriesListComponent {
  public categories$ = this.categoryService.getCategories$();

  constructor(private categoryService: CategoryService) { }
}
