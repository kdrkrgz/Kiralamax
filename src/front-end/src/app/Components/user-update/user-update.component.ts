import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserData } from 'src/app/Models/UserData';
import { CustomerService } from 'src/app/Services/customer.service';
import { JwtTokenService } from 'src/app/Services/jwt-token.service';
import { SnackbarService } from 'src/app/Services/snackbar.service';
import { UserService } from 'src/app/Services/user.service';
import { RegisterComponent } from '../register/register.component';
import { UserClaimsUpdateComponent } from '../user-claims-update/user-claims-update.component';

@Component({
  selector: 'app-user-update',
  templateUrl: './user-update.component.html',
  styleUrls: ['./user-update.component.css']
})
export class UserUpdateComponent implements OnInit {
  userUpdateForm:FormGroup;
  userData:UserData;
  isUserAdmin:boolean;

  constructor(
    public dialogRef: MatDialogRef<UserUpdateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UserData,
    private _formBuilder: FormBuilder,
    private snackBarService: SnackbarService,
    public dialog: MatDialog,
    private userService:UserService,
    private tokenService: JwtTokenService) {
  }

  ngOnInit() { 
    this.getUser(this.data.email);
    this.getUserAuthority();
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  createUserUpdateForm(){
    this.userUpdateForm = this._formBuilder.group({
      //customerId: ['', Validators.required],
      email:['', Validators.required],
      firstName: ['', Validators.required],
      lastName:['', Validators.required],
      status:['', Validators.required],
      companyName:['', Validators.required],
    })
  }

  getUser(email:string){
    this.userService.getUserData(email).subscribe(response => {
      this.userData = response.data;
      response.isSuccess ?this.createUserUpdateForm(): this.snackBarService.openErrorSnackBar(response.message);
    })
  }

  updateUser(){    
    if (!this.isUserAdmin) {
      this.userUpdateForm.controls['email'].setValue(this.data.email);
      this.userUpdateForm.controls['status'].setValue(this.data.status);
    }    
    if(this.userUpdateForm.valid){
      this.userService.updateUser(this.userData).subscribe(response => {
        this.userData = response.data
        this.snackBarService.openSnackBarWithMessage(response);
      })
    }

  }

  openOperationsClaimsDialog(): void{
    const dialogRef = this.dialog.open(UserClaimsUpdateComponent, {
      width: '300px',
      data: this.userData
    }
    );
    dialogRef.afterClosed().subscribe(res => {
    })
  }

  getUserAuthority(){
    let result = this.tokenService.checkUserAuthority(["admin", "system.admin"])
    if (result == true) {
      this.isUserAdmin = true
    }
    else{
      this.isUserAdmin=false
    }
  }
}
