import { Component, OnInit } from '@angular/core';
import { ListCategoriesModel } from '../../../models/categories/list-categories.model';
import { CategoryService } from '../../../services/category.service';

@Component({
  selector: 'app-all-categories',
  templateUrl: './all-categories.component.html'
})
export class AllCategoriesComponent implements OnInit {
  public categories: ListCategoriesModel[];

  constructor(private categoryService: CategoryService) { }

  ngOnInit() {
    this.categoryService
      .getCategories()
      .subscribe(res => this.categories = res);
  }
}