import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { ResponseModel } from '../Models/responseModel';
import { SingleResponseModel } from '../Models/singleResponseModel';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {

constructor(private _snackBar: MatSnackBar) { }


snackBar(message:string, action:string){
  this._snackBar.open(message, action, {
    duration: 2000,
  });
}

openSnackBar(message: string, action: string = "kapat") {
  this._snackBar.open(message, action, {
    duration: 3000,
    horizontalPosition: "right",
    verticalPosition: "top",
  });
}

openSuccessSnackBar(message: string, action: string = "kapat"){
  this._snackBar.open(message, action, {
    duration: 3000,
    horizontalPosition: "right",
    verticalPosition: "top",
    panelClass: 'success-snack'
  });
}

openErrorSnackBar(message: string, action: string = "kapat"){
  this._snackBar.open(message, action, {
    duration: 3000,
    horizontalPosition: "right",
    verticalPosition: "top",
    panelClass: 'error-snack'
  });
}

openSnackBarWithMessage(responseData:ResponseModel){
  responseData.isSuccess? this.openSuccessSnackBar(responseData.message) : this.openErrorSnackBar(responseData.message)  
}

}
