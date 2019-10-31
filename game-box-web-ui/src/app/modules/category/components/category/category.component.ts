import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Store, select } from '@ngrx/store';

import { takeWhile, filter } from 'rxjs/operators';

import { CategoryService } from '../../services/category.service';
import { FormService } from 'src/app/modules/core/services/form.service';
import { ActionType } from '../../../core/enums/action-type.enum';
import { IAppState } from 'src/app/store/app.state';
import { LoadCategoryToEdit, LoadCategoryNames } from 'src/app/store/categories/categories.actions';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html'
})
export class CategoryComponent implements OnInit, OnDestroy {
  private componentActive = true;
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
      this.store.dispatch(new LoadCategoryToEdit(this.categoryId));
      this.store.pipe(
        select(s => s.categories.toEdit),
        filter(c => !!c),
        takeWhile(() => this.componentActive)
      )
      .subscribe(category => this.categoryForm.setValue({
        name: category.name
      }));
    }
  }

  public ngOnDestroy(): void {
    this.componentActive = false;
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
    this.store.dispatch(new LoadCategoryNames());
    this.router.navigate(['/categories/all']);
  }
}
