import { Directive } from '@angular/core';
import { AbstractControl, FormGroup, ValidationErrors, NG_VALIDATORS, Validator, ValidatorFn } from '@angular/forms';

export const confirmPasswordValidator: ValidatorFn = (control: FormGroup): ValidationErrors | null => {
  const password = control.get('password');
  const confirmPassword = control.get('confirmPassword');
  let result = password && confirmPassword && password.value === confirmPassword.value ? null : { 'noPasswordConfirm': true };
  return result;
};

@Directive({
  selector: '[noPasswordConfirm]',
  providers: [{ provide: NG_VALIDATORS, useExisting: ConfirmPasswordDirective, multi: true }]
})
export class ConfirmPasswordDirective implements Validator {
  validate(control: AbstractControl): ValidationErrors {
    return confirmPasswordValidator(control)
  }
}
