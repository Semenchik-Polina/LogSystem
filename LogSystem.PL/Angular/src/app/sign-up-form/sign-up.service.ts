import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';

import { User } from '../shared/user';
import { log } from 'util';
import { Observable } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

const httpOptionsValidation = {
  headers: new HttpHeaders({
    'Content-Type': 'text/plain'
  })
};

@Injectable({
  providedIn: 'root'
})
export class SignUpService {

  baseUrl = 'SignUp/';

  constructor(private http: HttpClient) { }

  signUp(user: User): Observable<number> {
    const url = `${this.baseUrl}SignUp`;
    return this.http.post<number>(url, user, httpOptions);
  }

  isUserNameTaken(userName: string): Observable<boolean> {
    const url = `${this.baseUrl}IsUserNameTaken`;
    const body = { UserName: userName };
    log(`${url} ${body}`);
    return this.http.post<boolean>(url, body, httpOptions);
  }

}
