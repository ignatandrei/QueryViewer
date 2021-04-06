import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FieldDescription, SearchField } from './FieldDescription';

@Injectable({
  providedIn: 'root'
})
export class SearchDataService {

  private root: string ;
  constructor( private http: HttpClient) { 
    this.root=environment.url ;

  }


  public addSearch(item: string ,  query: string , data:SearchField[] ) : Observable<string>{
    const url :string  = this.root +item + "/SaveSearch"+ query ;
    var s={
      searches:data
    };
    
    // var headers = new HttpHeaders().set('Content-Type', 'text/plain; charset=utf-8');
    
    return this.http.post<string>(url,s, { responseType: 'text' as 'json' });
    
  }
  public getSearch(item: string ,  query: string ,key:string): Observable<SearchField[]>{
    
    const url :string  = this.root +item + "/GetSearch"+ query +"/"+key;
    return this.http.get<SearchField[]>(url);
    
  }


}
