import { MethodCall } from '@angular/compiler';
import { Inject, Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { of as observableOf } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { ConfirmComponent } from '../Components/confirm/confirm.component';
import { ConfirmModel } from '../Models/ConfirmModel';

@Injectable({
  providedIn: 'root'
})
export class ConfirmService {
  
  confirmComponent: ConfirmComponent;
  dialogRef: MatDialogRef<ConfirmComponent, any>


constructor(public dialog: MatDialog) { 
}

createConfirm(title:string): Observable<any>{

  this.dialogRef = this.dialog.open(ConfirmComponent, {
    width: '500px',
    maxHeight: 750,
    data: {title:title}
  });
  return this.dialogRef.afterClosed();
}

confirmResult(){

  
  // this.dialogRef.afterClosed().subscribe(result => {
  //   if (result == true) {
  //     return true;
  //   }else{
  //     return false;
  //   }
  // });
}

}
