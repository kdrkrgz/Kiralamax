import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ListResponseModel } from '../Models/listResponseModel';
import { ResponseModel } from '../Models/responseModel';
import { SingleResponseModel } from '../Models/singleResponseModel';
import { UserClaim } from '../Models/UserClaim';
import { UserData } from '../Models/UserData';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  baseUrl: string = environment.backend.baseURL;

  getUserData(userEmail:string):Observable<SingleResponseModel<UserData>>{
    return this.httpClient.get<SingleResponseModel<UserData>>(this.baseUrl + "users/" + userEmail)
  }

  getUsers():Observable<ListResponseModel<UserData>>{
    return this.httpClient.get<ListResponseModel<UserData>>(this.baseUrl + "users")
  }

  getUserClaims(userId:number):Observable<ListResponseModel<UserClaim>>{
    return this.httpClient.get<ListResponseModel<UserClaim>>(this.baseUrl + "users/userclaims/" + userId)
  }

  updateUser(userData:UserData):Observable<SingleResponseModel<UserData>>{
    return this.httpClient.put<SingleResponseModel<UserData>>(this.baseUrl + "users",  userData)
  }

  deleteUser(userEmail:string):Observable<ResponseModel>{
    return this.httpClient.delete<ResponseModel>(this.baseUrl + "users/"+ userEmail)
  }

  updateUserClaims(userEmail: string ,claimIds:number[]):Observable<ResponseModel>{
    return this.httpClient.post<ResponseModel>(this.baseUrl + "users/updateuserclaims", {useremail: userEmail, claimids:claimIds})
  }
}
