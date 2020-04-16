import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpResponse, HttpHandler, HttpEvent, HttpErrorResponse }
  from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';


@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor() { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    if (!request.headers.has('Content-Type')) {
      request = request.clone({ headers: request.headers.set('Content-Type', 'application/json') });
    }
    request = request.clone({ headers: request.headers.set('Accept', 'application/json') });

    return next.handle(request)
      .pipe(map(
        (event: HttpEvent<any>) => { return event }),
      catchError((errorResponse: HttpErrorResponse) => {
        let errorCode;
        let errorMessage;
        if (errorResponse.error instanceof Error) {
          // A client-side or network error occurred
          errorCode = errorResponse.status;
          errorMessage = errorResponse.message;
        } else {
          // The backend returned an unsuccessful response code.
          errorCode = 500;
          errorMessage = `Backend returned code ${errorResponse.status}, body was: ${errorResponse.error}`
        }
        window.alert(`An error occured. Status code: ${errorResponse.status} ${errorResponse.statusText}`);
       
        return throwError(errorResponse);
      }));
      
  }
}
