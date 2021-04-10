import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CreditScore } from '../Models/CreditScore';
import { SingleResponseModel } from '../Models/singleResponseModel';

@Injectable({
  providedIn: 'root'
})
export class CreditScoreService {

constructor(private httpClient: HttpClient) { }

baseUrl: string = environment.backend.baseURL;

getUserCreditScore():Observable<SingleResponseModel<CreditScore>>{
 return this.httpClient.get<SingleResponseModel<CreditScore>>(this.baseUrl + "creditscores")
}

}
