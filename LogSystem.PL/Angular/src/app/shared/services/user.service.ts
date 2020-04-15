import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { User } from '../models/user';

@Injectable(
{
  providedIn: 'root'
})
export class UserService {

  private baseUrl = "api/Users";

  constructor( private http: HttpClient) { }

  getUserByID(userID: number): Observable<User> {
    const getUrl = `${this.baseUrl}/${userID}`;
    return this.http.get<User>(getUrl);
  }

  getUserByUserName(userName: string): Observable<User> {
    const getUrl = `${this.baseUrl}/${userName}`;
    return this.http.get<User>(getUrl);
  }

  updateUser(id: number, user: User): Observable<number> {
    const putUrl = `${this.baseUrl}/${id}`;
    return this.http.put<number>(putUrl, user);
  }
}
