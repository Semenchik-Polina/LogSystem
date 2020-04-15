import { Directive } from '@angular/core';
import { AbstractControl, NG_ASYNC_VALIDATORS, ValidationErrors, FormControl } from '@angular/forms';
import { map, switchMap } from 'rxjs/operators';
import { timer } from 'rxjs';

import { UserService } from '../shared/services/user.service';

// after 0.5 sec check if this username is already taken
export const uniqueUserNameValidator = (userService: UserService, time: number = 500) => {
  return (input: FormControl) => {
    return timer(time).pipe(
      switchMap(() => userService.getUserByUserName(input.value)),
      map(res => {
        return res ? { userNameIsTaken: true } : null
      })
    );
  };
};


@Directive({
  selector: '[userNameIsTaken]',
  providers: [
    {
      provide: NG_ASYNC_VALIDATORS,
      useExisting: uniqueUserNameValidator,
      multi: true
    }
  ]
})
export class UniqueUserNameValidatorDirective {
  constructor(private validator: UniqueUserNameValidatorDirective) { }

  validate(control: AbstractControl): ValidationErrors {
    return this.validator.validate(control);
  }
}

