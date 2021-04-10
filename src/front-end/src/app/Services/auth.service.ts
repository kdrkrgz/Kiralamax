import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AccountModel } from '../Models/AccountModel';
import { LoginModel } from '../Models/LoginModel';
import { RegisterModel } from '../Models/RegisterModel';
import { ResponseModel } from '../Models/responseModel';
import { SingleResponseModel } from '../Models/singleResponseModel';
import { TokenModel } from '../Models/TokenModel';
import { UserData } from '../Models/UserData';
import { LocalStorageService} from '../Services/local-storage.service'

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl: string = environment.backend.baseURL;


  constructor(private httpClient: HttpClient,
    private localStorageService: LocalStorageService
    ) { }


  login(loginModel: LoginModel): Observable<SingleResponseModel<TokenModel>> {
    return this.httpClient.post<SingleResponseModel<TokenModel>>(this.baseUrl + "auth/login", loginModel)
  }

  register(registerModel:RegisterModel): Observable<SingleResponseModel<RegisterModel>> {  
    return this.httpClient.post<SingleResponseModel<RegisterModel>>(this.baseUrl + "auth/register", registerModel)
  }

  getUser(userId:number):Observable<SingleResponseModel<UserData>> {
    return this.httpClient.get<SingleResponseModel<UserData>>(this.baseUrl + "auth/" + userId)
  }

  forgotPassword(formData:FormData):Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(this.baseUrl + "auth/forgotpassword", formData)
  } 

  passwordReset(formData:FormData):Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(this.baseUrl + "auth/resetpassword", formData)
  }

  activateAccount(accountModel:AccountModel):Observable<ResponseModel>{
    return this.httpClient.post<ResponseModel>(this.baseUrl + "auth/activateaccount", accountModel)
  }

  isAuthenticated() {
    return this.localStorageService.getItem("token") ? true:false;
  }

  logout(){
    this.localStorageService.removeItem("token");
  }

  isExpired(){
    this.localStorageService.getItem("token")
  }

  private headers(){
    return new HttpHeaders().set('Content-Type','application/json');
  }

}
