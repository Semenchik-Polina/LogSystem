import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpResponse, HttpHandler, HttpEvent, HttpErrorResponse }
  from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

//import { ErrorDialogService } from '../error-dialog/error-dialog.service';


@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(/*public errorDialogService: ErrorDialogService*/) { }

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
        
        //let data = {
        //  reason: error && error.error && error.error.reason ? error.error.reason : '',
        //  status: error.status
        //};
        //const reason = error && error.error && error.error.reason ? error.error.reason : '';
        //const status = error.status;
        
        //console.log(`reason: ${reason} status: ${status}`);
        //console.log(error);
        //this.errorDialogService.openDialog(data);

        return throwError(errorResponse);
      }));

    //return next.handle(request).
    //  .catch((err: HttpErrorResponse) => {

    //    if (err.error instanceof Error) {
    //      // A client-side or network error occurred. Handle it accordingly.
    //      console.error('An error occurred:', err.error.message);
    //    } else {
    //      // The backend returned an unsuccessful response code.
    //      // The response body may contain clues as to what went wrong,
    //      console.error(`Backend returned code ${err.status}, body was: ${err.error}`);
    //    }

    //    // ...optionally return a default fallback value so app can continue (pick one)
    //    // which could be a default value
    //    return Observable.of(new HttpResponse({
    //      body: [
    //        { name: "Default values returned by Interceptor", id: 88 },
    //        { name: "Default values returned by Interceptor(2)", id: 89 }
    //      ]
    //    }));

    //    // or simply an empty observable
    //    // return Observable.empty<HttpEvent<any>>();
    //  });

      
  }
}
