import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from './auth.service';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class JwtTokenService {
  jwtHelper: JwtHelperService = new JwtHelperService();
  roles: string = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
  name: string = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
  nameIdentifier: string = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
  isAuthenticated:boolean;
  constructor(
    private localStodageService: LocalStorageService,
    private authService: AuthService
  ) {
    this.checkAuthentication();
   }

  decodeToken() {
    return this.isAuthenticated ? this.jwtHelper.decodeToken(this.localStodageService.getItem('token') as string): null;
  }

  getJwtUserEmail() {
    return this.isAuthenticated ? this.jwtHelper.decodeToken(this.localStodageService.getItem('token') as string).email: null;
  }

  getJwtUserRoles() {
    return this.isAuthenticated ? this.jwtHelper.decodeToken(this.localStodageService.getItem('token') as string)[this.roles]: null;
  }
  getJwtUserName() {
    return this.isAuthenticated ? this.jwtHelper.decodeToken(this.localStodageService.getItem('token') as string)[this.name]:null;
  }

  getJwtUserNameIdentifier() {
    return this.isAuthenticated ? this.jwtHelper.decodeToken(this.localStodageService.getItem('token') as string)[this.nameIdentifier]:null;
  }

  checkUserAuthority(authorityRoles: string[]) {
    if (this.isAuthenticated) {
      let userRoles = this.getJwtUserRoles();
      for (let i = 0; i < authorityRoles.length; i++) {
        if (userRoles.indexOf(authorityRoles[i]) > -1) {
          return true;
        }
      }
    }
    return null;

  }
  checkAuthentication(){
    if(this.authService.isAuthenticated()){
      this.isAuthenticated = true;
    }else {
      this.isAuthenticated = false;
    }
  }


}
