import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { SearchField } from './FieldDescription';
import { receivedData } from './receivedData';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private root: string ;
  constructor( private http: HttpClient) { 
    this.root=environment.url ;
  }
  public GetData(item:string,query:string, searches:SearchField[] ): Observable<receivedData|null>{
    if(searches == null || searches.length == 0)
      return of(null);

    var url = this.root+ item+'/DisplayDataFor'+ query;
    var s={ "searches": searches};
    // console.log(searches);
    return this.http.post<receivedData>(url,s).pipe(
      // tap(it=>console.log(it))
    );
  }

}
