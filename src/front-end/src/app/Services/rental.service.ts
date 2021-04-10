import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ListResponseModel } from '../Models/listResponseModel';
import { Rental } from '../Models/Rental';
import { ResponseModel } from '../Models/responseModel';

@Injectable({
  providedIn: 'root'
})
export class RentalService {

constructor(private httpClient: HttpClient) { }

baseUrl: string = environment.backend.baseURL;


GetRentals(): Observable<Rental[]> {
  return this.httpClient.get<Rental[]>(this.baseUrl + "rentals")
}

GetRentalsDataResult(): Observable<ListResponseModel<Rental>> {
  return this.httpClient.get<ListResponseModel<Rental>>(this.baseUrl + "rentals")
}

AddRent(formdata:FormData):Observable<ResponseModel>{
  return this.httpClient.post<ResponseModel>(this.baseUrl + "rentals", formdata)
}

ComplateRent(carId:number): Observable<ResponseModel>{
  return this.httpClient.put<ResponseModel>(this.baseUrl + "rentals/complete/" + carId, null)
}

DeleteRent(rentId:number):Observable<ResponseModel>{
  return this.httpClient.delete<ResponseModel>(this.baseUrl + "rentals/" + rentId);
}


}
