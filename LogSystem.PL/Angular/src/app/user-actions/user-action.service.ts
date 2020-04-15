import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { UserAction } from './models/user-action';


@Injectable({
  providedIn: 'root'
})

export class UserActionService {

  private baseUrl = "api/UserActions";

  constructor( private http: HttpClient ) { }

  // get all user actions
  // return array of UserAction
  getUserActions(): Observable<UserAction[]> {
    return this.http.get<UserAction[]>(this.baseUrl);
  }

}
