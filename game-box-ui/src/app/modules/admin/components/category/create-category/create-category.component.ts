import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators, AbstractControl } from '@angular/forms';
import { CategoryService } from '../../../services/category.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html'
})
export class CreateCategoryComponent implements OnInit {
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
    private router: Router
  ) { }

  ngOnInit() {
    this.categoryForm = this.fb.group({
      name: new FormControl('', [Validators.required, Validators.minLength(3)])
    });

    const nameControl = this.categoryForm.controls.name;
    nameControl
      .valueChanges
      .subscribe(() => {
        this.nameMessage = '';
        this.nameMessage = this.setMessage(nameControl, 'nameValidationMessage');
      });
  }

  setMessage(control: AbstractControl, messageKey: string): string {
    if ((control.touched || control.dirty) && control.errors) {
      return Object.keys(control.errors)
        .map(key => this.validationMessages[messageKey][key])
        .join(' ');
    }
  }

  createCategory(): void {
    this.categoryService
      .createCategory(this.categoryForm.value)
      .subscribe(() => this.router.navigate(['/admin/categories/all']));
  }
}