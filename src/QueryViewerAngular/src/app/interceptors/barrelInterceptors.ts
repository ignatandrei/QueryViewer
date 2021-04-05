import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorInterceptor } from './ErrorInterceptor';
import { LoaderInterceptor } from './LoaderInterceptor';





/** Http interceptor providers in outside-in order */
export const httpInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true },
];