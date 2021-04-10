import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'firstcharacter'
})
export class FirstcharacterPipe implements PipeTransform {

  transform(text: string, args?:any): string {
    text = text? text.toLocaleUpperCase(): "";
    return text.charAt(0);
  }

}
