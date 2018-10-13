import { FormGroup } from '@angular/forms';

export function matchingProperties(propertyName: string, targetPropertyName: string) {
  return (group: FormGroup): { [key: string]: any } => {
    let property = group.controls[propertyName];
    let targetProperty = group.controls[targetPropertyName];

    if (property.value !== targetProperty.value) {
      return {
        mismatchedPropertyValues: true
      };
    }
  }
}