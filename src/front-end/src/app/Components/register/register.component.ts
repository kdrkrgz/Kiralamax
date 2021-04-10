import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AuthService } from 'src/app/Services/auth.service';
import { SnackbarService } from 'src/app/Services/snackbar.service';
import { GlobalErrorStateMatcher } from 'src/app/utilities/globalErrorStateMatcher';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm:FormGroup;
  matcher = new GlobalErrorStateMatcher();

  constructor(
    public dialogRef: MatDialogRef<RegisterComponent>,
    @Inject(MAT_DIALOG_DATA) public data: null,
    private _formBuilder: FormBuilder,
    private authService: AuthService,
    private snackBarService: SnackbarService,
    public dialog: MatDialog,
  ) { }

  ngOnInit() {
    this.createRegisterForm();
  }

  createRegisterForm(){
    this.registerForm = this._formBuilder.group({
      email:new FormControl('', [Validators.required, Validators.email, Validators.maxLength(32)]),
      firstName:['', Validators.required],
      lastName:['', Validators.required],
      password: new FormControl('', [Validators.required, Validators.minLength(6), Validators.maxLength(32)]),
    })
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  register(){
    let registerModel = Object.assign({}, this.registerForm.value)
    this.authService.register(registerModel).subscribe(response => {
      response.isSuccess ? 
      this.snackBarService.openSuccessSnackBar("Kullanıcı oluşturuldu. Hesabınızı aktif etmek için mailinizi kontrol ediniz."):
      this.snackBarService.openErrorSnackBar("Kullanıcı oluşturulmadı.");
      
    }, responseErrors => {
      this.snackBarService.openErrorSnackBar(responseErrors.error)
      
    })
  }

}
