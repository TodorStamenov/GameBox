import { Component } from '@angular/core';

import { CategoryService } from '../../../services/category.service';

@Component({
  selector: 'app-all-categories',
  templateUrl: './all-categories.component.html'
})
export class AllCategoriesComponent {
  public categories$ = this.categoryService.getCategories$();

  constructor(private categoryService: CategoryService) { }
}
