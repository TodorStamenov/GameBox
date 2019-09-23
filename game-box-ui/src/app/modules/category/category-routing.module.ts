import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CategoriesListComponent } from '../category/components/categories-list/categories-list.component';
import { CategoryComponent } from '../category/components/category/category.component';

const categoryRoute: Routes = [
  { path: 'all', component: CategoriesListComponent },
  { path: ':action', component: CategoryComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(categoryRoute)
  ],
  exports: [
    RouterModule
  ]
})
export class CategoryRoutingModule { }
