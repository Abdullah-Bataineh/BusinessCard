import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, throwError } from 'rxjs';
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

  public GetAllBusinessCards(page:number,pageSize:number,name:string,email:string,phone:string,address:string,gender:string,year:string,month:string,day:string): Observable<HttpResponse<IBusinessCard>> {
    return this.http.get<IBusinessCard>(`${urls.BASEURL}${urls.BUSINESSCARDENDPOINT}?page=${page}&pageSize=${pageSize}&name=${name}&email=${email}&phone=${phone}&address=${address}&gender=${gender}&year=${year}&month=${month}&day=${day}`, { observe: 'response' })
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

  public ExportBusinessCard(BusinessCard: IBusinessCard, TypeFile: string): Observable<HttpResponse<any>> {
    return this.http.post<any>(
      `${urls.BASEURL}${urls.EXPORTBUSINESSCARD}${TypeFile}`,
      BusinessCard,
      {
        observe: 'response', // Important: Make sure to observe the response
        responseType: 'arraybuffer' as 'json' // Type assertion here to avoid the error
      }
    );
  }

  public GetBusinessCardById(id:number):Observable<HttpResponse<IBusinessCard>> {
    return this.http.get<IBusinessCard>(`${urls.BASEURL}${urls.BUSINESSCARDENDPOINT}/${id}`, { observe: 'response' });
  }
  public UpdateBusinessCard(BusinessCard:IBusinessCard):Observable<HttpResponse<any>>{
    return this.http.put<any>(`${urls.BASEURL}${urls.BUSINESSCARDENDPOINT}`,BusinessCard, { observe: 'response' });
  }
  getCountryCodeByDialCode(dialCode: string): Observable<string | undefined> {
    return this.http.get<any[]>('https://restcountries.com/v3.1/all').pipe(
      map(countries => {
        const country = countries.find(c => {
          if (c.idd && c.idd.suffixes) {
            const dialingCodes = c.idd.suffixes.map((suffix: any) => c.idd.root + suffix);
            return dialingCodes.includes(dialCode);
          }
          return false; 
        });
        return country ? country.cca2 : undefined; 
      })
    );
  }
}
