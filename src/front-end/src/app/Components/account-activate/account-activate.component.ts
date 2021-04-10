import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountModel } from 'src/app/Models/AccountModel';
import { AuthService } from 'src/app/Services/auth.service';
import { SnackbarService } from 'src/app/Services/snackbar.service';

@Component({
  selector: 'app-account-activate',
  templateUrl: './account-activate.component.html',
  styleUrls: ['./account-activate.component.css']
})
export class AccountActivateComponent implements OnInit {
  userEmail:string;
  activationCode:string;
  isFinished = false;
  constructor(private activatedRoute: ActivatedRoute,
    private router:Router,
    private snackbarService: SnackbarService,
    private authService: AuthService) { }

  ngOnInit() {
    this.getUrlData();
    this.activateAccount();
    this.returnHomePage();
  }

  getUrlData(){
    this.activatedRoute.params.subscribe(params => {
      this.userEmail = params["useremail"];
      this.activationCode = params["activationCode"];

      
   });
  }

  activateAccount(){

    
    
    
    if (this.userEmail != null && this.activationCode != null) {
      const accModel:  AccountModel = { userEmail: this.userEmail, code: this.activationCode};
      this.authService.activateAccount(accModel).subscribe(response => {
        this.isFinished = true;
        this.snackbarService.openSnackBarWithMessage(response)
      })
    }
  } 

  returnHomePage(){
  setTimeout(() => {
    this.router.navigateByUrl("/cars")
  }, 5000);
}
}
