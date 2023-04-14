import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';



@Injectable({
    providedIn: 'root'
})
export class DatabaseAdmin {
    baseUrl:string=environment.url;
    constructor(private http: HttpClient) { 

    }

    public getDatabases():Observable<string[]|never>{      
        var data=this.http.get<string[]>(this.baseUrl+'MetaData/DBNames');
        return data;
    }

}
