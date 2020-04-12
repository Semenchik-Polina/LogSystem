import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { confirmPasswordValidator } from '../shared/confirm-password.directive';
import { UserService } from '../user.service';
import { User } from '../shared/user';
import { log } from 'util';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { DISABLED } from '@angular/forms/src/model';

@Component({
  selector: 'edit-user-form',
  templateUrl: './edit-user-form.component.html',
  styleUrls: ['./edit-user-form.component.scss'],
  providers: [UserService]
})
export class EditUserFormComponent implements OnInit {

  id: number;
  private user: User;
  private editUserForm: FormGroup;
   typeList = ["User", "Admin"];
  private routeSubscription: Subscription;

  constructor(
    private _userService: UserService,
    private route: ActivatedRoute,
    private router: Router) {

    this.routeSubscription = route.params.subscribe(params => this.id = params['id']);
  }

  ngOnInit(): void {
    if (!this.id) {
      this.router.navigate(['/LogIn/LogIn']);
    }
    this._userService.getUser(this.id)
      .subscribe((user: User) => {
        this.user = user;
        this.user.RegistrationDate = new Date(parseInt(this.user.RegistrationDate.toString().substr(6)));
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

  onSubmit() {
    log(this.editUserForm.value);
    this._userService.updateUser(this.editUserForm.value)
      .subscribe((userId) => {
        userId ? location.reload() : this.router.navigate([]);
      });
  }

}
