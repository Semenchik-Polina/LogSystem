import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, NavigationEnd } from '@angular/router';

import { AuthService } from '../shared/services/auth.service';
import { UserCookieService } from '../shared/services/user-cookie.service';

@Component({
  selector: 'log-in-form',
  templateUrl: './log-in-form.component.html'
})
export class LogInFormComponent implements OnInit, OnDestroy {

  private loginForm: FormGroup;
  private navigationSubscription;

  constructor( private userCookieService: UserCookieService, private authService: AuthService, private router: Router) {
    this.navigationSubscription = this.router.events.subscribe((e: any) => {
      // if it's a NavigationEnd event re-initalise the component
      if (e instanceof NavigationEnd) {
        this.ngOnInit();
      }
    });
    this.checkAuthentication()
  }

  // init form
  ngOnInit(): void {
    this.loginForm = new FormGroup({
      'userName': new FormControl('', Validators.required),
      'password': new FormControl('', Validators.required)
    })
  }

  get userName() { return this.loginForm.get('userName'); }
  get password() { return this.loginForm.get('password'); }


  onSubmit() {
    this.authService.logIn(this.loginForm.value)
      .subscribe((userID) => {
        userID ? this.router.navigate([`/Users/${userID}`]) : this.router.navigate([]);
      });
  }


  // navigate to user-edit-form if user is already autheticated
  checkAuthentication() {
    console.log("checkAuth logIn");
    this.authService.getIsAuthenticated()
      .subscribe((isAuth: boolean) => {
        if (isAuth) {
          console.log("Auth is true logIn");
          const cookie = this.userCookieService.getCookieValue();
          if (cookie && cookie.UserID) {
            console.log("Navigate to user logIn");
            this.router.navigate([`/Users/${cookie.UserID}`]);
          }
        }
      })
  }

  ngOnDestroy() {
    // avoid memory leaks by cleaning the subscription up.
    if (this.navigationSubscription) {
      this.navigationSubscription.unsubscribe();
    }
  }
}
