import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { CategoryService } from '../../../services/category.service';
import { FormService } from 'src/app/modules/core/services/form.service';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html'
})
export class EditCategoryComponent implements OnInit {
  private categoryId: string;
  public categoryForm: FormGroup;

  get name() { return this.categoryForm.get('name'); }

  constructor(
    private fb: FormBuilder,
    private categoryService: CategoryService,
    private router: Router,
    private route: ActivatedRoute,
    public formService: FormService
  ) {
    this.categoryId = this.route.snapshot.params['id'];
  }

  public ngOnInit(): void {
    this.categoryForm = this.fb.group({
      'name': [null, [Validators.required, Validators.minLength(3)]]
    });

    this.categoryService
      .getCategory$(this.categoryId)
      .subscribe(res => this.categoryForm.setValue({ name: res.name }));
  }

  public editCategory(): void {
    this.categoryService
      .editCategory$(this.categoryId, this.categoryForm.value)
      .subscribe(() => this.router.navigate(['/admin/categories/all']));
  }
}
