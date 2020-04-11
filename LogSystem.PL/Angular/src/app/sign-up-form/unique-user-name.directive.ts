import { Directive, forwardRef, Injectable } from '@angular/core';
import { AsyncValidator, AbstractControl, NG_ASYNC_VALIDATORS, ValidationErrors, ValidatorFn, FormControl } from '@angular/forms';
import { catchError, map, switchMap } from 'rxjs/operators';
import { SignUpService } from './sign-up.service';
import { timer, Observable, of } from 'rxjs';

//@Injectable({ providedIn: 'root' })
//export class UniqueUserNameValidator implements AsyncValidator {
//  constructor(private signUpService: SignUpService) { }

//  validate(
//    ctrl: AbstractControl
//  ): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> {
//    return this.signUpService.isUserNameTaken(ctrl.value).pipe(
//      map(isTaken => (isTaken ? { uniqueUserName: true } : null)),
//      catchError(() => of(null))
//    );
//  }
//}

export const uniqueUserNameValidator = (signUpService: SignUpService, time: number = 500) => {
  return (input: FormControl) => {
    return timer(time).pipe(
      switchMap(() => signUpService.isUserNameTaken(input.value)),
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

