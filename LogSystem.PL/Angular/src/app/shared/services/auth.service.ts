import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { LogInUser } from '../../log-in-form/log-in-user';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  private baseUrl = 'api/';
  
  constructor(private http: HttpClient) { }

  // send post request to server to log in the user
  // return logged in user id
  logIn(user: LogInUser): Observable<number> {
    const logInUrl = `${this.baseUrl}LogIn`;
    return this.http.post<number>(logInUrl, user);
  }

  // send post request to server to log out the user
  logOut(): Observable<any> {
    const logOutUrl = `${this.baseUrl}LogOut`;
    return this.http.post(logOutUrl, null);
  }

  // send post request to server to sign up the user
  // return signed up user id
  signUp(user: User): Observable<number> {
    const signUpUrl = `${this.baseUrl}SignUp`;
    return this.http.post<number>(signUpUrl, user);
  }

  // get is the user authenticated
  getIsAuthenticated(): Observable<boolean> {
    const isAuthUrl = `${this.baseUrl}Users/isAuthenticated`; 
    return this.http.get<boolean>(isAuthUrl);
  }
}
