import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { SnackbarService } from 'src/app/Services/snackbar.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {

  passwordResetForm:FormGroup;
  userEmail:string;
  activationCode:string;

  constructor(private activatedRoute: ActivatedRoute,
    private _formBuilder: FormBuilder,
    private snackbarService: SnackbarService,
    private authService: AuthService,
    private router:Router) { 
      this.getUrlData();
    }



  ngOnInit() {
    this.createPassworResetForm();
  }

  getUrlData(){
    this.activatedRoute.params.subscribe(params => {
      this.userEmail = params["useremail"];
      this.activationCode = params["activationCode"];
   });
  }

  createPassworResetForm(){
    this.passwordResetForm = this._formBuilder.group({
      password:['', Validators.required]
    })
  }

  resetPassword(){    
    if(this.passwordResetForm.valid){
      let formData = new FormData();
      formData.append("password", this.passwordResetForm.controls['password'].value)
      formData.append("userEmail", this.userEmail)
      formData.append("code", this.activationCode)
      
      this.authService.passwordReset(formData).subscribe(response => {
        this.snackbarService.openSnackBarWithMessage(response);
        this.router.navigateByUrl("/cars")
      })
    }
  }


}
