import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../admin.service';
import { CategoryModel } from '../../models/category.model';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html'
})
export class CategoryComponent implements OnInit {
  public categories: CategoryModel[];

  constructor(private adminService: AdminService) { }

  ngOnInit() {
    this.adminService
      .getCategories()
      .subscribe(res => this.categories = res);
  }
}