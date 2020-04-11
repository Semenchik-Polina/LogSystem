import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';

import { AuthorizationViewModel } from './log-in-user';
import { User } from '../shared/user';
import { log } from 'util';
import { Observable } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class LogInService {

  baseUrl = 'LogIn/';

  constructor(private http: HttpClient) { }

  logIn(user: AuthorizationViewModel): Observable<User> {
    const url = `${this.baseUrl}LogIn`;
    return this.http.post<User>(url, user, httpOptions);
  }

}
