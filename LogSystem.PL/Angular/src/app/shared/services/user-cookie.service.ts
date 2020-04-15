import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

import { UserCookie } from '../models/user-cookie';

@Injectable({
  providedIn: 'root'
})
export class UserCookieService {

  private userCookieName = "User";

  constructor(private cookieService: CookieService) { }

  getCookieValue(): UserCookie {
    if (this.cookieService.check(this.userCookieName)) {
      const cookieStr = this.cookieService.get(this.userCookieName);
      let cookie = {};
      // parse cookie string
      cookieStr.split('&').forEach((property) => {
        var val = property.split('=');
        cookie[val[0]] = val[1];
      });
      return  new UserCookie((+cookie["UserID"]), cookie["UserType"]);
    }
    else {
      return undefined;
    }
  }
}
