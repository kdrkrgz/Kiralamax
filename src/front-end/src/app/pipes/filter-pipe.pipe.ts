import { Pipe, PipeTransform } from '@angular/core';
import { Car } from '../Models/Car';

@Pipe({
  name: 'filterPipe'
})
export class FilterPipePipe implements PipeTransform {


  transform(value: Car[], filterText: string): Car[] {
    filterText = filterText?filterText.toLocaleLowerCase():"";
    return filterText?value.filter((x:Car) => (x.carModel.toLocaleLowerCase().indexOf(filterText)! == -1)):value;
  }

}

// map, reduce, filter
