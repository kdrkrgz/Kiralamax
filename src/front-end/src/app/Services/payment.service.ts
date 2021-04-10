import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CreditCard } from '../Models/CreditCard';
import { ResponseModel } from '../Models/responseModel';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  baseUrl: string = environment.backend.baseURL;


  constructor(private httpClient:HttpClient ) { }
  
  
  
  pay(creditCard:CreditCard):Observable<ResponseModel>{
    return this.httpClient.post<ResponseModel>(this.baseUrl + "payments", creditCard)
  }

}
