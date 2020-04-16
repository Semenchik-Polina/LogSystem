import { Component, OnDestroy } from '@angular/core';
import { AuthService } from '../shared/services/auth.service';
import { UserCookie } from '../shared/models/user-cookie';
import { UserCookieService } from '../shared/services/user-cookie.service';
import { Router, NavigationStart } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnDestroy {

  navbarOpen = false;
  private navigationSubscription;
  private isAuthenticated: boolean;
  private cookieValue: UserCookie;

  constructor(private userCookieService: UserCookieService, private authService: AuthService,
    private router: Router) {

    // if it's a NavigationStart event update cookie and check if the user is authenticated
    this.navigationSubscription = this.router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
        this.updateIsAuthenticated();
        this.updateCookieValue();
      }
    });
  }

  toggleNavbar() {
    this.navbarOpen = !this.navbarOpen;
  }

  logOut() {
    this.authService.logOut()
      .subscribe(() => {
        this.updateIsAuthenticated();
        this.router.navigate(['/LogIn']);
      });
  }

  updateCookieValue() {
    this.cookieValue = this.userCookieService.getCookieValue();
  }

  updateIsAuthenticated() {
    this.authService.getIsAuthenticated()
      .subscribe((isAuth: boolean) =>
        this.isAuthenticated = isAuth);
  }

  ngOnDestroy() {
    // avoid memory leaks by cleaning the subscription up.
    if (this.navigationSubscription) {
      this.navigationSubscription.unsubscribe();
    }
  }

}
