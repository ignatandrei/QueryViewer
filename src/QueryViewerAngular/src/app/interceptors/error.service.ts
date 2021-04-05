import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {

  private err: Subject<string>;

  constructor() {
    this.err = new Subject<string>();
  }

  public NextError(): Observable<string> {

    return this.err.asObservable();

  }

  public setNextError(str: string) {

    this.err.next(str);

  }

}
