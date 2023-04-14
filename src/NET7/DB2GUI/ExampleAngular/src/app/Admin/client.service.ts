import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ApplicationDBContext_Client_Table } from './ApplicationDBContext_Client_Table';



@Injectable({
  providedIn: 'root'
})
export class ClientService {
  addClient(newClient: ApplicationDBContext_Client_Table): Observable<number> {
    var url=environment.url +'REST_ApplicationDBContext_Client';
    return this.http.post<number>(url, newClient);
  }

  constructor(private http: HttpClient) { }

  public getClients():Observable<ApplicationDBContext_Client_Table[]> {
    var url=environment.url +'REST_ApplicationDBContext_Client';
    return this.http.get<ApplicationDBContext_Client_Table[]> (url);
  }

}
