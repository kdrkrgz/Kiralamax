import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserClaim } from 'src/app/Models/UserClaim';
import { UserData } from 'src/app/Models/UserData';
import { CustomerService } from 'src/app/Services/customer.service';
import { SnackbarService } from 'src/app/Services/snackbar.service';
import { UserClaimsService } from 'src/app/Services/user-claims.service';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-user-claims-update',
  templateUrl: './user-claims-update.component.html',
  styleUrls: ['./user-claims-update.component.css']
})
export class UserClaimsUpdateComponent implements OnInit {

  userClaimsForm : FormGroup;
  userData: UserData;
  userClaims: UserClaim[];
  availableUserClaims: UserClaim[];
  claims: FormControl;
  availableUserClaimIds: number[] = [];

  constructor(
    public dialogRef: MatDialogRef<UserClaimsUpdateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UserData,
    private _formBuilder: FormBuilder,
    private snackBarService: SnackbarService,
    public dialog: MatDialog,
    private userService:UserService,
    private userClaimService:UserClaimsService) {
      this.userData = this.data;
      this.getUserClaims(this.data.id);
  }
  ngOnInit() {
    this.getAllClaims();
    //this.getUserClaims(this.data.id);
    this.createUserClaimsForm();
  }
  onNoClick(): void {    
    this.dialogRef.close();
  }

  createUserClaimsForm(){
    this._formBuilder.group({
      claims: new FormControl({value:this.availableUserClaims},[Validators.required])
    })
    
  }

  getAllClaims(){
    this.userClaimService.getClaims().subscribe(response => {
      this.userClaims = response.data;
    })
  }

  getUserClaims(userId:number){
    this.userService.getUserClaims(userId).subscribe(response => {
      this.availableUserClaims = response.data;
      // gelen datayı claimlerin olduğu {id:claimId} listesinie maple
      let claimIds = this.availableUserClaims.map(x => ({id:x?.id}));
      this.claims = new FormControl();
      // claimIdleri bir number arraya at
      claimIds.forEach((x) => { this.availableUserClaimIds.push(x.id)}); 
      // number arrayı foromcontrole gönder   
      this.claims.setValue(this.availableUserClaimIds);
      
    })
  }

  updateUserClaims(){
    this.userService.updateUserClaims(this.data.email, this.claims.value).subscribe(response => {
      this.snackBarService.openSnackBarWithMessage(response)
    })
  }

}
