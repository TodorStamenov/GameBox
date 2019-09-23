import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Store, select } from '@ngrx/store';

import { CategoryService } from '../../services/category.service';
import { FormService } from 'src/app/modules/core/services/form.service';
import { ActionType } from '../../../core/enums/action-type.enum';
import { ICategoryBindingModel } from '../../models/category-binding.model';
import { IAppState } from 'src/app/store/app.state';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html'
})
export class CategoryComponent implements OnInit {
  private categoryId: string | undefined;
  private actionType: ActionType | undefined;

  public categoryForm: FormGroup;

  get name() { return this.categoryForm.get('name'); }

  constructor(
    private fb: FormBuilder,
    private categoryService: CategoryService,
    private router: Router,
    private route: ActivatedRoute,
    private store: Store<IAppState>,
    public formService: FormService
  ) {
    this.actionType = ActionType[<string>this.route.snapshot.params['action']];
    this.categoryId = this.route.snapshot.queryParams['id'];
  }

  public ngOnInit(): void {
    this.categoryForm = this.fb.group({
      'name': [null, [Validators.required, Validators.minLength(3)]]
    });

    if (this.actionType === ActionType.edit) {
      this.categoryService
        .getCategory$(this.categoryId)
        .subscribe(() => {
          this.store.pipe(
            select(state => state.categories.toEdit)
          )
          .subscribe((category: ICategoryBindingModel) => this.categoryForm.setValue({
            name: category.name
          }));
        });
    }
  }

  public saveCategory(): void {
    if (this.actionType === ActionType.create) {
      this.categoryService
        .createCategory$(this.categoryForm.value)
        .subscribe(() => this.navigateToAllCategories());
    } else if (this.actionType === ActionType.edit) {
      this.categoryService
        .editCategory$(this.categoryId, this.categoryForm.value)
        .subscribe(() => this.navigateToAllCategories());
    }
  }

  private navigateToAllCategories(): void {
    this.router.navigate(['/categories/all']);
  }
}
