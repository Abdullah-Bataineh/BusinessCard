import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { typeFile } from 'src/app/Enum/TypeFile';
import { urls } from 'src/app/Enum/URLS';
import { IBusinessCard } from 'src/app/Model/IBusinessCard';

@Injectable({
  providedIn: 'root'
})
export class BCService {

  constructor(private http: HttpClient) { }

  public CreateBusinessCard(body: IBusinessCard): Observable<HttpResponse<any>> {
    return this.http.post<any>(
      `${urls.BASEURL}${urls.BUSINESSCARDENDPOINT}`,
      body,
      { observe: 'response' }
    );
  }

  public GetAllBusinessCards(): Observable<HttpResponse<IBusinessCard>> {
    return this.http.get<IBusinessCard>(`${urls.BASEURL}${urls.BUSINESSCARDENDPOINT}`, { observe: 'response' })
  }
  public FileImportBusinessCard(formData: FormData): Observable<HttpResponse<IBusinessCard>> {
    return this.http.post<IBusinessCard>(
      `${urls.BASEURL}${urls.FILEUPLOADBUSINESSCARD}`,
      formData,
      { observe: 'response' })
  }

  public DeleteBusinessCard(id: any): Observable<HttpResponse<any>> {
    return this.http.delete<any>(`${urls.BASEURL}${urls.BUSINESSCARDENDPOINT}/${id}`, { observe: 'response' });
  }

  public ExportBusinessCard(BusinessCard: IBusinessCard, TypeFile: string): Observable<HttpResponse<ArrayBuffer>> {
    return this.http.post<ArrayBuffer>(
      `${urls.BASEURL}${urls.EXPORTBUSINESSCARD}${TypeFile}`,
      BusinessCard,
      {
        observe: 'response', // Important: Make sure to observe the response
        responseType: 'arraybuffer' as 'json' // Type assertion here to avoid the error
      }
    );
  }
}
