import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'sliceString'
})
export class SliceStringPipe implements PipeTransform {
  public transform(value: string, length: number = 200): string {
    if (0 < length && length < value.length) {
      return value.substring(0, length) + '...';
    }

    return value;
  }
}
