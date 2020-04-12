import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { UserAction } from './user-action';
import { log } from 'util';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  })
};

@Injectable({
  providedIn: 'root'
})

export class UserActionService {

  private baseUrl = "api/UserAction1";

  constructor( private http: HttpClient ) { }

  getUserActions(): Observable<UserAction[]> {
    //const getUrl = `${this.baseUrl}Get`;
    return this.http.get<UserAction[]>(this.baseUrl, httpOptions);
  }

}
