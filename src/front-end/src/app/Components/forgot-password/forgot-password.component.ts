import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AuthService } from 'src/app/Services/auth.service';
import { CategoryService } from 'src/app/Services/category.service';
import { JwtTokenService } from 'src/app/Services/jwt-token.service';
import { SnackbarService } from 'src/app/Services/snackbar.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  forgotPasswordForm: FormGroup;


  constructor(public dialogRef: MatDialogRef<ForgotPasswordComponent>,
    @Inject(MAT_DIALOG_DATA) public data: null,
    private _formBuilder: FormBuilder,
    private authService: AuthService,
    private categoryService: CategoryService,
    private snackbarService: SnackbarService,
    public dialog: MatDialog) { }

  ngOnInit() {    
    this.createForgotPasswordForm();
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  createForgotPasswordForm(){
    this.forgotPasswordForm = this._formBuilder.group({
      emailAddress: ['',Validators.required]
    })
  }

  sendFotgotPasswordForm(){
    if (this.forgotPasswordForm.valid) {
      const formData = new FormData();
      formData.append("userEmail", this.forgotPasswordForm.get('emailAddress')?.value);
      this.authService.forgotPassword(formData).subscribe(response => {
        this.snackbarService.openSnackBarWithMessage(response)
      })
    }

  }
}
