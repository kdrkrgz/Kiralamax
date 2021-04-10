import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ListResponseModel } from '../Models/listResponseModel';
import { UserClaim } from '../Models/UserClaim';

@Injectable({
  providedIn: 'root'
})
export class UserClaimsService {

  constructor(private httpClient: HttpClient) { }

  baseUrl: string = environment.backend.baseURL;


  getClaims(): Observable<ListResponseModel<UserClaim>> {
    return this.httpClient.get<ListResponseModel<UserClaim>>(this.baseUrl + "operationclaims")
  }


}
