import { Injectable } from '@angular/core';
import { AbstractControl } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class FormService {
  public setMessage(control: AbstractControl, messageKey: string, validationMessages: any): string {
    if (control.touched && control.errors) {
      return Object.keys(control.errors)
        .map(key => validationMessages[messageKey][key])
        .join(' ');
    }
  }

  public setPasswordMessage(control: AbstractControl, targetControl: AbstractControl, fieldName: string, targetFieldName: string): string {
    if (control.value !== targetControl.value) {
      return `${fieldName} and ${targetFieldName} fields should have same value!`;
    }
  }
}
