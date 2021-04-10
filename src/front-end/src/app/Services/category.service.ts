import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Category } from '../Models/Category';
import { ListResponseModel } from '../Models/listResponseModel';
import { ResponseModel } from '../Models/responseModel';
import { SingleResponseModel } from '../Models/singleResponseModel';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

constructor(private httpClient: HttpClient) { }



baseUrl: string = environment.backend.baseURL;


GetCategories(): Observable<ListResponseModel<Category>> {
  return this.httpClient.get<ListResponseModel<Category>>(this.baseUrl + "categories")
}

AddCategory(category: Category): Observable<ResponseModel> {
  return this.httpClient.post<ResponseModel>(this.baseUrl + "categories", category);
}

UpdateCategory(category: Category): Observable<ResponseModel> {
  return this.httpClient.put<ResponseModel>(this.baseUrl + "categories", category);
}

DeleteCategory(categoryId: number ): Observable<ResponseModel> {
  return this.httpClient.delete<ResponseModel>(this.baseUrl + "categories/delete/" + categoryId);
}

}