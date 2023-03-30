import { map, of} from "rxjs";
import { ajax } from 'rxjs/ajax';
//import { fromFetch } from "rxjs/fetch";
import { Observable } from "rxjs/internal/Observable";
import columnTable from "./column";
export default class DatabaseAdmin {
    // static const subject = new Subject();
    // const cancel = new Subject();
    baseUrl:string = '';
    constructor() { 
      this.baseUrl=process.env.REACT_APP_URL+'api/'; 
    }
    public getDatabases():Observable<string[]|never>{
        // var data= fromFetch(this.baseUrl+'MetaData/DBNames')
        var data=ajax.getJSON(this.baseUrl+'MetaData/DBNames')
        .pipe(
            map(response => {

                return response as string[];
            })
            //takeUntil(cancel)
          );
          return data;
    }

    public getDatabaseTables(id:string):Observable<string[]|never>{
        // var data= fromFetch(this.baseUrl+'MetaData/DBNames')
        if(id.length === 0) return of([] as string[]); 
        var data=ajax.getJSON(this.baseUrl+`MetaData/TableNames/${id}`)
        .pipe(
            map(response => {

                return response as string[];
            })
            //takeUntil(cancel)
          );
          return data;
    }
    public getTableRowsNumber(idDB:string,idTable:string):Observable<number|never>{
        if(idDB.length === 0) return of(-1 ); 
        if(idTable.length === 0) return of(-1) ;         
        var data=ajax.getJSON(this.baseUrl+`AdvancedSearch_${idDB}_${idTable}/GetAllCount/`)
        .pipe(
            map(response => {

                return response as number;
            })
            //takeUntil(cancel)
          );
          return data;
    }
    
    public getDatabaseTableColumns(idDB:string,idTable:string):Observable<columnTable[]>{
        if(idDB.length === 0) return of([] as columnTable[]); 
        if(idTable.length === 0) return of([] as columnTable[]);         
        var data=ajax.getJSON(this.baseUrl+`MetaData/Columns/${idDB}/${idTable}`)
        .pipe(
            map(response => {

                return response as columnTable[];
            })
            //takeUntil(cancel)
          );
          return data;
    }
    

}

