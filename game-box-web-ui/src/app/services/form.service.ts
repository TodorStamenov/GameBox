import { Injectable } from '@angular/core';
import { AbstractControl } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class FormService {
  public getErrorMessage(control: AbstractControl, key: string): string {
    const messageKey = Object.keys(control.errors)[0];

    switch (messageKey) {
      case 'required':
        return `${key} is required!`;
      case 'minlength':
        return `${key} should be at least ${control.errors.minlength.requiredLength} symbols long!`;
      case 'maxlength':
        return `${key} should be less than ${control.errors.maxlength.requiredLength} symbols long!`;
      case 'min':
        return `${key} should more than ${control.errors.min.min}`;
      case 'max':
        return `${key} should less than ${control.errors.max.max}`;
    }
  }

  public getMismatchFieldMessage(fieldName: string, targetFieldName: string): string {
    return `${fieldName} and ${targetFieldName} fields should have same value!`;
  }
}
