import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { LogInService } from './log-in.service';
import { log } from 'util';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'log-in-form',
  templateUrl: './log-in-form.component.html',
  styleUrls: ['./log-in-form.component.scss']
})
export class LogInFormComponent implements OnInit, OnDestroy {

  loginForm: FormGroup;
  navigationSubscription;

  constructor(private logInService: LogInService, private router: Router) {
    this.navigationSubscription = this.router.events.subscribe((e: any) => {
      // If it is a NavigationEnd event re-initalise the component
      if (e instanceof NavigationEnd) {
        this.ngOnInit();
      }
    });
  }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      'userName': new FormControl('', Validators.required),
      'password': new FormControl('', Validators.required)
    })
  }
  
  get userName() { return this.loginForm.get('userName'); }
  get password() { return this.loginForm.get('password'); }

  onSubmit() {
    this.logInService.logIn(this.loginForm.value)
      .subscribe((user) => {
        user ? this.gotoUserEdit(user.UserID) : this.router.navigate([]);
      });
    //location.reload();
  }

  gotoUserEdit(id: number) {
    //this.router.navigate([`/User/Edit/${id}`]);
    //this.router.navigate(['/Main/GetUserProfile']);
    log("reload is coming");
    location.assign('/Main/GetUserProfile');
  }

  ngOnDestroy() {
    // avoid memory leaks here by cleaning up after ourselves. If we  
    // don't then we will continue to run our initialiseInvites()   
    // method on every navigationEnd event.
    if (this.navigationSubscription) {
      this.navigationSubscription.unsubscribe();
    }
  }

}
