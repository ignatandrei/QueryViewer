import { Injectable } from '@angular/core';
import {
  HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse
} from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ErrorService } from './error.service';

/** Pass untouched request through to the next request handler. */
@Injectable()
export class ErrorInterceptor implements HttpInterceptor {


  constructor(private err: ErrorService ) {


  }
  intercept(req: HttpRequest<any>, next: HttpHandler):
    Observable<HttpEvent<any>> {

    return next.handle(req).pipe(
        catchError( err => this.handleError(req, err))
    );
  }

  private handleError(req: HttpRequest<any>, error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      const str = 'An error occurred:' +   error.error.message;
      console.log(str);
      this.err.setNextError(str);

    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      const str =
        `Backend returned code ${error.status}, ` +
        `body was: ${JSON.stringify(error.error)}`;
      console.log(str);
      this.err.setNextError(str);
    }
    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
  }
}
