import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CreditCard } from '../Models/CreditCard';
import { ListResponseModel } from '../Models/listResponseModel';
import { ResponseModel } from '../Models/responseModel';

@Injectable({
  providedIn: 'root'
})
export class CreditCardService {

  baseUrl: string = environment.backend.baseURL;


  constructor(private httpClient:HttpClient ) { }
  
  
  getCustomerCreditCards(customerId: number):Observable<ListResponseModel<CreditCard>>{
    return this.httpClient.get<ListResponseModel<CreditCard>>(this.baseUrl + "creditcards/" + customerId);
  }

  deleteCustomerCreditCard(customerId:number):Observable<ResponseModel>{
    return this.httpClient.delete<ResponseModel>(this.baseUrl + "creditcards/delete/" + customerId);
  }

}
