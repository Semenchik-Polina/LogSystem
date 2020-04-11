import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { User } from './shared/user';
import { log } from 'util';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  })
};

@Injectable(
{
  providedIn: 'root'
})
export class UserService {

  private baseUrl = "User/";

  constructor( private http: HttpClient) {
  }

  getUser(userID: number): Observable<User> {
    const getUrl = `${this.baseUrl}Get/`
    const paramId = `${userID}`;
    const options = userID ?
      { params: new HttpParams().set('id', paramId) } : {};
    return this.http.get<User>(getUrl, options);
  }

  updateUser(user: User): Observable<number> {
    log(`${user.UserID}`);
    const putUrl = `${this.baseUrl}Edit/${user.UserID}`;
    log(putUrl);
    //const paramId = `${user.UserID}`;
    //const options = user ?
    //  { params: new HttpParams().set('id', paramId) } : {};
    return this.http.post<number>(putUrl, user, httpOptions)
  }
}
