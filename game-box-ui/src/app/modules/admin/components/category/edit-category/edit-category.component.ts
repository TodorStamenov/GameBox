import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, FormControl, Validators, AbstractControl } from '@angular/forms';

import { Subscription } from 'rxjs';

import { CategoryService } from '../../../services/category.service';
import { FormService } from 'src/app/sharedServices/form.service';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html'
})
export class EditCategoryComponent implements OnInit, OnDestroy {
  private subscription = new Subscription();
  private categoryId: string;
  public categoryForm: FormGroup;

  public nameMessage: string;

  private validationMessages = {
    nameValidationMessage: {
      required: 'Name is required',
      minlength: 'Name should be at least 3 symbols long!'
    }
  };

  constructor(
    private fb: FormBuilder,
    private categoryService: CategoryService,
    private router: Router,
    private route: ActivatedRoute,
    private formService: FormService
  ) {
    this.categoryId = this.route.snapshot.params['id'];
  }

  public ngOnInit(): void {
    this.categoryForm = this.fb.group({
      'name': new FormControl('', [Validators.required, Validators.minLength(3)])
    });

    this.categoryService
      .getCategory$(this.categoryId)
      .subscribe(res => this.categoryForm.setValue({ name: res.name }));

    const nameControl = this.categoryForm.controls.name;
    this.subscription.add(nameControl
      .valueChanges
      .subscribe(() => {
        this.nameMessage = '';
        this.nameMessage = this.formService.setMessage(nameControl, 'nameValidationMessage', this.validationMessages);
      }));
  }

  public ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  public editCategory(): void {
    this.categoryService
      .editCategory$(this.categoryId, this.categoryForm.value)
      .subscribe(() => this.router.navigate(['/admin/categories/all']));
  }
}
