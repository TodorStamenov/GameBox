import { Router } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

import { Subscription } from 'rxjs';

import { CategoryService } from '../../../services/category.service';
import { FormService } from 'src/app/sharedServices/form.service';

@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html'
})
export class CreateCategoryComponent implements OnInit, OnDestroy {
  private subscription = new Subscription();

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
    private formService: FormService
  ) { }

  public ngOnInit(): void {
    this.categoryForm = this.fb.group({
      'name': new FormControl('', [Validators.required, Validators.minLength(3)])
    });

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

  public createCategory(): void {
    this.categoryService
      .createCategory$(this.categoryForm.value)
      .subscribe(() => this.router.navigate(['/admin/categories/all']));
  }
}
