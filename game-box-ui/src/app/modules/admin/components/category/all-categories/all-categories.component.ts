import { Component, OnInit } from '@angular/core';
import { CategoryModel } from '../../../models/categories/category.model';
import { CategoryService } from '../../../services/category.service';

@Component({
  selector: 'app-all-categories',
  templateUrl: './all-categories.component.html'
})
export class AllCategoriesComponent implements OnInit {
  public categories: CategoryModel[];

  constructor(private categoryService: CategoryService) { }

  ngOnInit() {
    this.categoryService
      .getCategories()
      .subscribe(res => this.categories = res);
  }
}