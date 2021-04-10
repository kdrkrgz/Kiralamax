import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'tryPipe'
})
export class TryPipePipe implements PipeTransform {

  transform(value: string, currencySymbol: string) {
    return value + " " + currencySymbol
  }

}
