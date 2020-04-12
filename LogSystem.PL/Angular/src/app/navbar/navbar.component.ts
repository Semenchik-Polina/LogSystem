import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  private userCookieName = "User";
  curUserID: number;
  curUserType: string;

  constructor(private cookieService: CookieService ) {
   
  }

  ngOnInit() {
    console.log(`userCookie exists: ${this.cookieService.check(this.userCookieName)}`);
    console.log(`cookie value: ${this.cookieService.get(this.userCookieName)}`);

    const cookieStr = this.cookieService.get(this.userCookieName);
    let cookie = {};
    cookieStr.split('&').forEach((property) => {
      var val = property.split('=');
      cookie[val[0]] = val[1];
    })
    console.log(cookie);
    this.curUserID = cookie["UserID"];
    this.curUserType = cookie["UserType"];
    console.log(this.curUserID);
    //var UserID = cookie

    console.log(`admin: ${this.curUserID && this.curUserType == 'Admin'}`);

  }



}
