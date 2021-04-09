import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of, ReplaySubject, Subscription, zip } from 'rxjs';
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
  
  public Init(): Promise<any> {
    var int32  = this.GetSearch("System.Int32") ;
    var int64 = this.GetSearch("System.Int64");
    var str = this.GetSearch("System.String");   
    var double = this.GetSearch("System.Double");   
    var dt = this.GetSearch("System.DateTime");
    var data = zip(int32,int64,str,double,dt)
      .pipe(
        tap(        
        ([int32Data,int64Data,strData,doubleData,dtData]) =>  
    {
        MetadataService.kvArr.set("System.Int32".replace(".","_"),int32Data);
        MetadataService.kvArr.set("System.Int64".replace(".","_"),int64Data);
        MetadataService.kvArr.set("System.String".replace(".","_"),strData);
        MetadataService.kvArr.set("System.Double".replace(".","_"),doubleData);
        MetadataService.kvArr.set("System.DateTime".replace(".","_"),dtData);
    }));
    return data.toPromise();
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

    var key = typeField.replace(".","_");
    if(MetadataService.kvArr.has(key))
      return of(MetadataService.kvArr.get(key)||[]);

      console.log("not found"+ typeField + MetadataService.kvArr.has(key));
      console.log(MetadataService.kvArr);
      var url = this.root+'GetSearch/'+ typeField;
    return this.http.get<KeyValue[]>(url)
      .pipe(
        tap(it=>
          {
          MetadataService.kvArr.set(typeField,it);
          //console.log("!!!!"+MetadataService.kvArr.has(typeField))
          }
          ),        
      )
    ;
  }
}
