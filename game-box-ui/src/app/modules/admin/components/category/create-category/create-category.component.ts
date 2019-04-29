import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

import { CategoryService } from '../../../services/category.service';
import { FormService } from 'src/app/modules/core/services/form.service';

@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html'
})
export class CreateCategoryComponent implements OnInit {
  public categoryForm: FormGroup;

  get name() { return this.categoryForm.get('name'); }

  constructor(
    private fb: FormBuilder,
    private categoryService: CategoryService,
    private router: Router,
    public formService: FormService
  ) { }

  public ngOnInit(): void {
    this.categoryForm = this.fb.group({
      'name': [null, [Validators.required, Validators.minLength(3)]]
    });
  }

  public createCategory(): void {
    this.categoryService
      .createCategory$(this.categoryForm.value)
      .subscribe(() => this.router.navigate(['/admin/categories/all']));
  }
}
