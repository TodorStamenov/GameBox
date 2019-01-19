import { FormGroup } from '@angular/forms';

export function matchingProperties(propertyName: string, targetPropertyName: string) {
  return (group: FormGroup): { [key: string]: any } => {
    const property = group.controls[propertyName];
    const targetProperty = group.controls[targetPropertyName];

    if (property.value !== targetProperty.value) {
      return {
        mismatchedPropertyValues: true
      };
    }
  };
}
