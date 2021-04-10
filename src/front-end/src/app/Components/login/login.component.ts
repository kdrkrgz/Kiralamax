import { DatePipe } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LoginModel } from 'src/app/Models/LoginModel';
import { AuthService } from 'src/app/Services/auth.service';
import { LocalStorageService } from 'src/app/Services/local-storage.service';
import { SnackbarService } from 'src/app/Services/snackbar.service';
import { BnNgIdleService } from 'bn-ng-idle';
import { Router } from '@angular/router';
import { IdleLogoutComponent } from '../idle-logout/idle-logout.component';
import { UserService } from 'src/app/Services/user.service';
import { JwtTokenService } from 'src/app/Services/jwt-token.service';
import { ForgotPasswordComponent } from '../forgot-password/forgot-password.component';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  isLinear = true;
  loginForm: FormGroup;
  tokenExpirationDate?: Date;
  logOutTimer: number = 1000;
  //loginModel: Login;
  loggedInUserEmail: string; // dialog kapanmadan önce tanımlanması gerekiyor. Amaç Nav componente kullanıcı emailini göndermek

  constructor(
    public dialogRef: MatDialogRef<LoginComponent>,
    @Inject(MAT_DIALOG_DATA) public data: LoginModel,
    private _formBuilder: FormBuilder,
    private authService: AuthService,
    private snackBarService: SnackbarService,
    private localStorageService: LocalStorageService,
    private datePipe: DatePipe,
    private bnIdle: BnNgIdleService,
    private router: Router,
    public dialog: MatDialog,
    private userService: UserService,
  ) { }


  ngOnInit(): void {
    this.createLoginForm();
  }

  createLoginForm() {
    this.loginForm = this._formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  // BPs
  login() {
    if (this.loginForm.valid) {
      let loginModel = Object.assign({}, this.loginForm.value) as LoginModel
      this.authService.login(loginModel).subscribe(response => {
        response.isSuccess ? this.snackBarService.openSuccessSnackBar("Başarıyla giriş yapıldı."):this.snackBarService.openErrorSnackBar("Kullanıcı adı veya parola hatalı.");
        this.localStorageService.setItem("token", response.data.token)

        response.isSuccess ? window.location.reload():null;        
        // nav componente giriş yapan kullanıcının emailini gönder
       //  response.isSuccess ? this.loggedInUserEmail = this.loginForm.get("email")?.value: null;
       
        // Idle Logout and Token operations
        this.setTokenExpirationDate(response.data.expiration)
        response.isSuccess ? this.checkUserIdleTime(this.authService.isAuthenticated()) : false;
        //
      }, responseError => {
        this.snackBarService.openErrorSnackBar(responseError.error)
      })
    }
  }

  openForgotPasswordDialog(){
    const dialogRef = this.dialog.open(ForgotPasswordComponent, {
      maxHeight: 750,
      data: true
    }
    );  }



  // Token Expiration and Idle Operations 
  setTokenExpirationDate(expirationDate: string) {
    let stringDate: any = this.datePipe.transform(expirationDate, 'yyyy-MM-dd hh:mm:ss');
    this.tokenExpirationDate = new Date(stringDate);
  }
  // Idle operations => sorun var
  // yeni bir idle dialog yazılacak ve idle geldikten sonra 10 saniye içinde basılmazsa çıkış işlemi o kısımda yapılacak
  // confirm sayfaları blokluyor.
  checkUserIdleTime(authStatus: boolean) {
    if (authStatus) {
      this.bnIdle.startWatching(300).subscribe((isTimedOut: boolean) => {

        const dialogRef = this.dialog.open(IdleLogoutComponent, {
          width: '500px',
          data: false
        }
        );

        dialogRef.afterClosed().subscribe(result => {
          if (result) {
            this.bnIdle.resetTimer;
          } else {
            this.authService.logout();
            this.bnIdle.stopTimer();
            this.router.navigate([this.router.url])
          }
        });
      })
    }
  }



}
