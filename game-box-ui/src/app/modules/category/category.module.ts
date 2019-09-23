import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { CategoryRoutingModule } from './category-routing.module';
import { CategoryComponent } from './components/category/category.component';
import { CategoriesListComponent } from './components/categories-list/categories-list.component';

@NgModule({
  declarations: [
    CategoriesListComponent,
    CategoryComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    CategoryRoutingModule
  ]
})
export class CategoryModule { }
