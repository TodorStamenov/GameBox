import { PipeTransform, Pipe } from '@angular/core';

@Pipe({
  name: 'sliceString'
})
export class SliceStringPipe implements PipeTransform {
  private defaultLength = 200;

  transform(value: string, length: number) {
    if (length && length < value.length) {
      return value.substring(0, length) + '...';
    }

    if (!length && this.defaultLength < value.length) {
      return value.substring(0, this.defaultLength) + '...';
    }

    return value;
  }
}
