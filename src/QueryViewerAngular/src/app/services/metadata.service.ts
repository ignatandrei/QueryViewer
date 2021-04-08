import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of, ReplaySubject, Subscription } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { shareReplay, switchMap, tap } from 'rxjs/operators';
import { ItemPlusQueries } from './ItemPlusQueries';
import { FieldDescription } from './FieldDescription';
import { KeyValue } from './KeyValue';

@Injectable({
  providedIn: 'root'
})
export class MetadataService {

  private root: string ;
  constructor( private http: HttpClient) { 
    this.root=environment.url +'Metadata/';

  }
  
  public exposeItems(): Observable<string[]>{
    var url = this.root+'ControllerNames';
    return this.http.get<string[]>(url)
    ;
  }
  public exposeFields(item:string, query:string): Observable<FieldDescription[]>{
    var url = this.root+'Fields/'+item+'/'+query;
    return this.http.get<FieldDescription[]>(url)
      .pipe(
        switchMap(it => {   
          var send = it.map(f=> new FieldDescription(f));       
          return of(send);
        }),
      );

  }
  
  public GetSql(item: string, query:string): Observable<string>{
    var url = this.root+'Definition/'+item +'/'+ query;
    return this.http.get<string>(url, { responseType: 'text' as 'json' });
      
  }
  public exposeItemWithQuery(item: string):Observable<ItemPlusQueries>{

    var url = this.root+'ActionsNames/'+item;
    return this.http.get<string[]>(url)
      .pipe(
        switchMap(it  => {
          const itemQ=new ItemPlusQueries();
          itemQ.item=item;
          itemQ.queries=it;
          return of(itemQ);

        }),
      )
      ;
    ;
  }
  private static kvArr= new Map<string,KeyValue[]>();

  public GetSearch(typeField: string): Observable<KeyValue[]>{

    if(MetadataService.kvArr.has(typeField))
      return of(MetadataService.kvArr.get(typeField)||[]);

      var url = this.root+'GetSearch/'+ typeField;
    return this.http.get<KeyValue[]>(url)
      .pipe(
        tap(it=>MetadataService.kvArr.set(typeField,it)),        
      )
    ;
  }
}
