import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { confirmPasswordValidator } from '../shared/confirm-password.directive';
import { SignUpService } from './sign-up.service';
import { uniqueUserNameValidator } from './unique-user-name.directive';
import { Router } from '@angular/router';


@Component({
  selector: 'sign-up-form',
  templateUrl: './sign-up-form.component.html',
  styleUrls: ['./sign-up-form.component.scss']
})
export class SignUpFormComponent implements OnInit {

  signUpForm: FormGroup;

  constructor(
    private signUpService: SignUpService,
    private router: Router) { }

  ngOnInit(): void {
    this.signUpForm = new FormGroup({
      'userName': new FormControl('', Validators.required, uniqueUserNameValidator(this.signUpService)),
      'firstName': new FormControl('', Validators.pattern("^[A-Za-z,.'-]+$")),
      'lastName': new FormControl('', Validators.pattern("^[A-Za-z,.'-]+$")),
      'password': new FormControl('', [Validators.required, Validators.pattern("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{6,}$")]),
      'confirmPassword': new FormControl('', Validators.required),
      'email': new FormControl('', [Validators.required, Validators.pattern(".+@.+\\..+")]),
      'type': new FormControl('User'),
    }, confirmPasswordValidator);
  }

  get userName() { return this.signUpForm.get('userName'); }
  get firstName() { return this.signUpForm.get('firstName'); }
  get lastName() { return this.signUpForm.get('lastName'); }
  get password() { return this.signUpForm.get('password'); }
  get confirmPassword() { return this.signUpForm.get('confirmPassword'); }
  get email() { return this.signUpForm.get('email'); }
  get type() { return this.signUpForm.get('type'); }

  onSubmit() {
    this.signUpService.signUp(this.signUpForm.value)
      .subscribe((newUserId) => {
        newUserId ? this.router.navigate(['/LogIn/LogIn']) : this.router.navigate([]);
      });
  }
}
