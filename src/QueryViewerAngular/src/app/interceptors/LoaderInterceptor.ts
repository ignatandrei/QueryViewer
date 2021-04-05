import { Injectable } from '@angular/core';
import {
  HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse
} from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError, tap, finalize } from 'rxjs/operators';

import { LoaderService } from './loader.service';

/** Pass untouched request through to the next request handler. */
@Injectable()
export class LoaderInterceptor implements HttpInterceptor {

  private requests: HttpRequest<any>[] = [];
  constructor(private ls: LoaderService ) {


  }
  removeRequest(req: HttpRequest<any>) {
    const i = this.requests.indexOf(req);
    if (i >= 0) {
      this.requests.splice(i, 1);
    }
    this.ls.isLoading(this.requests.length > 0);
  }
  // https://grensesnittet.computas.com/loading-status-in-angular-done-right/
  intercept(req: HttpRequest<any>, next: HttpHandler):
    Observable<HttpEvent<any>> {
    this.requests.push(req);
    this.ls.isLoading(true);
    return next.handle(req).pipe(
        finalize(() => this.removeRequest(req))
    );
  }

}
