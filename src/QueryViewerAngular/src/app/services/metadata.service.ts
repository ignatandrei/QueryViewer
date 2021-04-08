import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of, ReplaySubject, Subscription } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { shareReplay, tap } from 'rxjs/operators';
import { ItemPlusQueries } from './ItemPlusQueries';
import { FieldDescription } from './FieldDescription';
import { KeyValue } from './KeyValue';

@Injectable({
  providedIn: 'root'
})
export class MetadataService {

  private stateitems =new BehaviorSubject<string[]>([]);
  private actions=new BehaviorSubject<ItemPlusQueries>(new ItemPlusQueries());
  private fields=new BehaviorSubject<FieldDescription[]>([]);
  private root: string ;
  constructor( private http: HttpClient) { 
    this.root=environment.url +'Metadata/';

  }
  public exposeItemWithQuery(): Observable<ItemPlusQueries>{
    return this.actions.asObservable();
  }

  public exposeItems(): Observable<string[]>{
    return this.stateitems.asObservable();
  }
  public exposeFields(): Observable<FieldDescription[]>{
    return this.fields.asObservable();
  }
  
  public GetItems(): Subscription{
    var url = this.root+'ControllerNames';
    return this.http.get<string[]>(url)
      .pipe(
        //shareReplay({refCount: true, bufferSize: 1}),
        tap(it => this.stateitems.next(it)),
        //shareReplay({refCount: true, bufferSize: 1})
      )
      .subscribe();
    ;
  }
  public GetSql(item: string, query:string): Observable<string>{
    var url = this.root+'Definition/'+item +'/'+ query;
    return this.http.get<string>(url, { responseType: 'text' as 'json' });
      
  }
  public GetQueries(item: string):Subscription{

    var url = this.root+'ActionsNames/'+item;
    return this.http.get<string[]>(url)
      .pipe(
        tap(it => {
          const itemQ=new ItemPlusQueries();
          itemQ.item=item;
          itemQ.queries=it;
          this.actions.next(itemQ);

        }),
      )
      .subscribe();
    ;
  }
  public GetFields(item:string,query:string):Subscription{
    var url = this.root+'Fields/'+item+'/'+query;
    return this.http.get<FieldDescription[]>(url)
      .pipe(
        tap(it => {   
          var send = it.map(f=> new FieldDescription(f));       
          this.fields.next(send);
        }),
      )
      .subscribe();
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
