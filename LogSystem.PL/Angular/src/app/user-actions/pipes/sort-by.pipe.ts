import { Pipe, PipeTransform } from '@angular/core';
import { UserAction } from '../models/user-action';

@Pipe({
  name: 'sortBy'
})
export class SortByPipe implements PipeTransform {

  transform(userActions: UserAction[], sortBy?: string): UserAction[] {
    return userActions.sort((a, b) => {
      return  (a[sortBy] < b[sortBy]) ? -1 : (a[sortBy] > b[sortBy]) ? 1 : 0;
    })
  }

}
