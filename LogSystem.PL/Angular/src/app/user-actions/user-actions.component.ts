import { Component, NgModule, OnInit, OnDestroy  } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { UserAction } from './user-action';
import { UserActionService } from './user-action.service';
import { FilterModel } from './filter-model';
import { log } from 'util';

@Component({
  selector: 'user-actions',
  templateUrl: './user-actions.component.html',
  styleUrls: ['./user-actions.component.scss'],
  providers: [UserActionService],
})
export class UserActionsComponent implements OnInit, OnDestroy {

  navigationSubscription;
  userActions: UserAction[];
  filteredUserActions: UserAction[];
  filterByOptions = ['UserName', 'Type', 'Date'];
  typeOptions = ['Log in', 'Log out', 'Sign up', 'Update'];
  sortBy = "UserName";
  model = new FilterModel( 'UserName' );

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
        this.userActions.forEach(action =>
          action.Date = new Date(parseInt(action.Date.toString().substr(6))));
        this.filteredUserActions = this.userActions;
      });
  }

  getUserActions(): void {
    this.userActions = [
      { UserActionID: 1, Type: 0, UserName: "Cleo", Date: new Date('December 17, 1995 03:24:00') },
      { UserActionID: 2, Type: 1, UserName: "Kate", Date: new Date('December 20, 2005 03:24:00') },
      { UserActionID: 3, Type: 0, UserName: "Amy", Date: new Date('December 20, 2015 03:24:00') }
    ]
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
        this.reload();
        break;
      }
    }
    log(`model: ${JSON.stringify(this.model)}`);
    log(`filteredUserActions: ${JSON.stringify(this.filteredUserActions)}`);
    log(`userActions: ${JSON.stringify(this.userActions)}`);
  }

  clearFilter() {
    this.filteredUserActions = this.userActions;
  }

  // onClick button ??
  reload() {

  }

  filterByType(type: number): void {
    if (!!type) {
      this.filteredUserActions = this.userActions.filter(userAction =>
        userAction.Type == type);
    }
  }

  filterByUserName(userName: string): void {
    if (!!userName) {
      this.filteredUserActions = this.userActions.filter(userActions =>
        userActions.UserName.toLowerCase().includes(userName.toLowerCase()));
    }
  }

  filterByDate(moment: Date): void {
    if (!!moment) {
      var date = new Date(moment);
      this.filteredUserActions = this.userActions.filter(userAction =>
        this.datesAreOnSameDay(userAction.Date, date));
    }
  }

  datesAreOnSameDay(first: Date, second: Date): boolean {
    log(`first: ${first}`);
    log(`second: ${second}`);
    return first.getFullYear() === second.getFullYear() &&
      first.getMonth() === second.getMonth() &&
      first.getDate() === second.getDate();
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
