import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable} from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Car } from '../Models/Car';
import { ListResponseModel } from '../Models/listResponseModel';
import { ResponseModel } from '../Models/responseModel';



@Injectable({
  providedIn: 'root'
})
export class CarService {

  constructor(private httpClient: HttpClient) {
  }

  baseUrl: string = environment.backend.baseURL;


  GetCars(): Observable<ListResponseModel<Car>> {
    return this.httpClient.get<ListResponseModel<Car>>(this.baseUrl + "cars")
  }

  GetCarsDataResult(): Observable<ListResponseModel<Car>> {
    return this.httpClient.get<ListResponseModel<Car>>(this.baseUrl + "cars")
  }

  // AddCar(car:Car):Observable<ResponseModel> {
  //   return this.httpClient.post<ResponseModel>(this.baseUrl+ "cars", car)
  // }

  DeleteCar(carId:number):Observable<ResponseModel>{
    return this.httpClient.delete<ResponseModel>(this.baseUrl + "cars/delete/"+ carId);
  }

  // Best Practise
  AddCar(formData:FormData):Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(this.baseUrl + "cars", formData)
  }

  DeleteCarPhoto(photoId:number):Observable<ResponseModel> {   
    return this.httpClient.delete<ResponseModel>((this.baseUrl + "cars/deletecarphoto/"+ photoId));
  }
}


