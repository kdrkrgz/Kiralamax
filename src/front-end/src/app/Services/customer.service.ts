import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Customer } from '../Models/Customer';
import { ListResponseModel } from '../Models/listResponseModel';
import { SingleResponseModel } from '../Models/singleResponseModel';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

baseUrl: string = environment.backend.baseURL;


constructor(private httpClient:HttpClient ) { }



getCustomers():Observable<ListResponseModel<Customer>>{
  return this.httpClient.get<ListResponseModel<Customer>>(this.baseUrl + "customers")
}

getCustomer(customerEmail:string):Observable<SingleResponseModel<Customer>>{
  return this.httpClient.get<SingleResponseModel<Customer>>(this.baseUrl + "customers/" + customerEmail)
}

}

