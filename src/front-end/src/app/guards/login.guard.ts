import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../Services/auth.service';
import { SnackbarService } from '../Services/snackbar.service';

@Injectable({
  providedIn: 'root'
})
export class LoginGuard implements CanActivate {

  /**
   *
   */
  constructor(private authService: AuthService, private snackBarService: SnackbarService) {
    
  }
  // Best Practise
  
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      if (this.authService.isAuthenticated()) {
        return true;
      }else {
        this.snackBarService.openErrorSnackBar("Bu işlemi yapabilmek için giriş yapmalısınız.")
        return false;
      }
  }
  
}
