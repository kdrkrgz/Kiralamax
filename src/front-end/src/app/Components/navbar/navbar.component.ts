import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { MatDialog } from '@angular/material/dialog';
import { LoginComponent } from '../login/login.component';
import { AuthService } from 'src/app/Services/auth.service';
import { SnackbarService } from 'src/app/Services/snackbar.service';
import { RegisterComponent } from '../register/register.component';
import { UserService } from 'src/app/Services/user.service';
import { LoginModel } from 'src/app/Models/LoginModel';
import { JwtTokenService } from 'src/app/Services/jwt-token.service';
import { UserData } from 'src/app/Models/UserData';
import { UserUpdateComponent } from '../user-update/user-update.component';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  isAuthenticated: boolean;
  loggedInUserModel: UserData;
  randomColor: string = "#5BDE06";
  // auth
  isUserAdmin: boolean;

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );
  constructor(private breakpointObserver: BreakpointObserver,
    private authService: AuthService,
    public dialog: MatDialog,
    private snackBarService: SnackbarService,
    private userService: UserService,
    private jwtTokenService: JwtTokenService
  ) {
    this.loggedInStatus();
    this.getUserAuthority();
  }

  ngOnInit() {
    this.getRandomColor();
    this.getLoggedInUserDetails();
  }

  // LoginComponent
  openLoginDialog(): void {
    const dialogRef = this.dialog.open(LoginComponent, {
      width: '300px',
      data: [{ isLoggedIn: this.isAuthenticated }]
    }
    );
    dialogRef.afterClosed().subscribe(result => {
      this.loggedInStatus();
      this.getLoggedInUserDetails();
    })

  }

  logout() {
    this.authService.logout();
    this.snackBarService.openSnackBar("Başarıyla çıkış yapıldı.")
    this.loggedInStatus();
    window.location.reload();
  }

  loggedInStatus() {
    this.isAuthenticated = this.authService.isAuthenticated()
  }


  getLoggedInUserDetails() {
    var email = this.jwtTokenService.getJwtUserEmail();
    this.userService.getUserData(email).subscribe(response => {
      this.loggedInUserModel = response.data
    })
  }

  openRegisterDialog(): void {
    const dialogRef = this.dialog.open(RegisterComponent, {
      width: '300px',
      data: null
    }
    );
    dialogRef.afterClosed().subscribe(res => {
    })
  }

  getRandomColor() {
    let colorList = ["#00af91", "#d44000", "#be0000", "#b67162", "#cdc733", "#bb8082", "#93329e", "#7868e6"]
    this.randomColor = colorList[Math.floor(Math.random() * colorList.length)]
  }

  openUserUpdateDialog() {
    const dialogRef = this.dialog.open(UserUpdateComponent, {
      width: "300px",
      data: this.loggedInUserModel
    })
    dialogRef.afterClosed().subscribe(res => {
    })
  }

  // Check roles 
getUserAuthority() {
  if(this.jwtTokenService.checkUserAuthority(["admin", "system.admin"]) == true ){
   this.isUserAdmin = true
  }else{
   this.isUserAdmin = false;
  }
}

}
