import { Component, OnInit, OnDestroy  } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { FilterModel } from './models/filter-model';

import { UserAction } from './models/user-action';
import { UserActionService } from './user-action.service';

@Component({
  selector: 'user-actions',
  templateUrl: './user-actions.component.html',
  providers: [UserActionService],
})
export class UserActionsComponent implements OnInit, OnDestroy {

  private navigationSubscription;
  private userActions: UserAction[];
  private filteredUserActions: UserAction[];

  private filterByOptions = ['UserName', 'Type', 'Date'];
  private typeOptions = ['Log in', 'Log out', 'Sign up', 'Update'];
  private sortBy = "UserName";
  private model = new FilterModel( 'UserName' );

  constructor(private userActionService: UserActionService, private router: Router) {
    this.navigationSubscription = this.router.events.subscribe((e: any) => {
      // If it is a NavigationEnd event re-initalise the component
      if (e instanceof NavigationEnd) {
        this.ngOnInit();
      }
    });
  }

  ngOnInit(): void {
    this.userActionService.getUserActions()
      .subscribe((actions: UserAction[]) => {
        this.userActions = actions;
        this.filteredUserActions = this.userActions;
      });
  }

  filter() {
    switch (this.model.filterBy) {
      case 'UserName': {
        this.filterByUserName(this.model.userName);
        this.model.userName = null;
        break;
      };
      case 'Type': {
        this.filterByType(this.model.type);
        this.model.type = null;
        break;
      };
      case 'Date': {
        this.filterByDate(this.model.date);
        this.model.date = null;
        break;
      }
      default: {
        break;
      }
    }
  }

  clearFilter() {
    this.filteredUserActions = this.userActions;
  }

  private filterByType(type: number): void {
    if (!!type) {
      this.filteredUserActions = this.userActions.filter(userAction =>
        userAction.Type == type);
    }
  }

  private filterByUserName(userName: string): void {
    if (!!userName) {
      this.filteredUserActions = this.userActions.filter(userActions =>
        userActions.UserName.toLowerCase().includes(userName.toLowerCase()));
    }
  }

  private filterByDate(moment: Date): void {
    if (!!moment) {
      var date = new Date(moment);
      this.filteredUserActions = this.userActions.filter(userAction =>
        this.datesAreOnSameDay(userAction.Date, date));
    }
  }

  // check if two dates are on the same date
  // return boolean
  private datesAreOnSameDay(first: Date, second: Date): boolean {
    return first.getFullYear() === second.getFullYear() &&
      first.getMonth() === second.getMonth() &&
      first.getDate() === second.getDate();
  }

  ngOnDestroy() {
    // avoid memory leaks here by cleaning the navigation subscription up 
    if (this.navigationSubscription) {
      this.navigationSubscription.unsubscribe();
    }
  }
}
