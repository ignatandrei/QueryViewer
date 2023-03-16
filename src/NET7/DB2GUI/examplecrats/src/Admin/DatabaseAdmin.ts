import { concatMap, map, of, switchMap, tap } from "rxjs";
import { ajax } from 'rxjs/ajax';
import { fromFetch } from "rxjs/fetch";
import { Observable } from "rxjs/internal/Observable";
export default class DatabaseAdmin {
    // static const subject = new Subject();
    // const cancel = new Subject();

    public getDatabases():Observable<string[]|never>{
        // var data= fromFetch('http://localhost:5018/MetaData/DBNames')
        var data=ajax.getJSON('http://localhost:5018/MetaData/DBNames')
        .pipe(
            map(response => {

                return response as string[];
            })
            //takeUntil(cancel)
          );
          return data;
    }

    

}

