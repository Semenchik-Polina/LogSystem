import { Pipe, PipeTransform } from '@angular/core';
import { UserAction } from '../user-action';
import { log } from 'util';

@Pipe({
  name: 'sortBy'
})
export class SortByPipe implements PipeTransform {

  transform(userActions: UserAction[], sortBy?: string): UserAction[] {
    log(`sortBy: ${sortBy}`);
    return userActions.sort((a, b) => {
      return  (a[sortBy] < b[sortBy]) ? -1 : (a[sortBy] > b[sortBy]) ? 1 : 0;
    })
  }

}
