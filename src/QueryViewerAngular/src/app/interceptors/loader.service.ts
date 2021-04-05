
import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {
  constructor() {
    this.s = new Subject<boolean>();

  }

  public s: Subject<boolean>;
  public loading$(): Observable<boolean> {
    return this.s.asObservable();
  } 

  public isLoading(b: boolean) {
    this.s.next(b);
  }

}
