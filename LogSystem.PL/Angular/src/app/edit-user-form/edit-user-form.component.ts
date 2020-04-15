import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

import { User } from '../shared/models/user';
import { confirmPasswordValidator } from '../shared/confirm-password.directive';
import { UserService } from '../shared/services/user.service';


@Component({
  selector: 'edit-user-form',
  templateUrl: './edit-user-form.component.html',
  providers: [UserService]
})
export class EditUserFormComponent implements OnInit {

  private id: number;
  private typeList = ["User", "Admin"];
  private user: User;
  private editUserForm: FormGroup;
  private routeSubscription: Subscription;

  constructor( private userService: UserService, private route: ActivatedRoute,
    private router: Router) {

    // get id from request parameters
    this.routeSubscription = route.params.subscribe(
      params => this.id = params['id']);
  }

  ngOnInit(): void {
    if (!this.id) {
      this.router.navigate(['']);
    }
    // if id is defined get user by id
    this.userService.getUserByID(this.id)
      .subscribe((user: User) => {
        this.user = user;
        this.initForm();
      }); 
  }

  get userID() { return this.editUserForm.get('userID'); }
  get userName() { return this.editUserForm.get('userName'); }
  get firstName() { return this.editUserForm.get('firstName'); }
  get lastName() { return this.editUserForm.get('lastName'); }
  get password() { return this.editUserForm.get('password'); }
  get confirmPassword() { return this.editUserForm.get('confirmPassword'); }
  get email() { return this.editUserForm.get('email'); }
  get type() { return this.editUserForm.get('type'); }
  get registrationDate() { return this.editUserForm.get('registrationDate'); }

  initForm() {
    this.editUserForm = new FormGroup({
      'userID': new FormControl(this.user.UserID),
      'userName': new FormControl({ value: this.user.UserName, disabled : true }),
      'firstName': new FormControl(this.user.FirstName, Validators.pattern("^[A-Za-z,.'-]+$")),
      'lastName': new FormControl(this.user.LastName, Validators.pattern("^[A-Za-z,.'-]+$")),
      'password': new FormControl('',Validators.pattern("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{6,}$")),
      'confirmPassword': new FormControl(''),
      'email': new FormControl(this.user.Email, Validators.pattern(".+@.+\\..+")),
      'type': new FormControl(this.user.Type),
      'registrationDate': new FormControl({value: this.user.RegistrationDate, disabled : true })
    }, confirmPasswordValidator);
  }

  // on "save" button update the profile 
  onSubmit() {
    this.userService.updateUser(this.id, this.editUserForm.value)
      .subscribe((userId) => {
        this.router.navigate([]);
      });
  }


}
