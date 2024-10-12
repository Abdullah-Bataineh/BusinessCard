import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { urls } from 'src/app/Enum/URLS';
import { IBusinessCard } from 'src/app/Model/IBusinessCard';

@Injectable({
  providedIn: 'root'
})
export class BCService {

  constructor(private http:HttpClient) { }

  public CreateBusinessCard(body: IBusinessCard): Observable<HttpResponse<any>> {
    return this.http.post<any>(
      `${urls.BASEURL}${urls.BUSINESSCARDENDPOINT}`,
      body,
      { observe: 'response' }
    );
  }
  public GetAllBusinessCards():Observable<HttpResponse<IBusinessCard>>{
  return this.http.get<IBusinessCard>(`${urls.BASEURL}${urls.BUSINESSCARDENDPOINT}`,{ observe: 'response' })
}
}
