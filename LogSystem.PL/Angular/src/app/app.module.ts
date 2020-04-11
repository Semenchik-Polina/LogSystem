import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { AngularFontAwesomeModule} from 'angular-font-awesome';
import bootstrap from "bootstrap";
import { DpDatePickerModule } from 'ng2-date-picker';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EditUserFormComponent } from './edit-user-form/edit-user-form.component';
import { LogInFormComponent } from './log-in-form/log-in-form.component';
import { SignUpFormComponent } from './sign-up-form/sign-up-form.component';
import { UserActionsComponent } from './user-actions/user-actions.component';
import { ConfirmPasswordDirective } from './shared/confirm-password.directive';
//import { UniqueUserNameValidator } from './sign-up-form/unique-user-name.directive';

import { LogInService } from './log-in-form/log-in.service';
import { UserService } from './user.service';
import { SignUpService } from './sign-up-form/sign-up.service';
import { UserActionService } from './user-actions/user-action.service';
import { SortByPipe } from './user-actions/pipes/sort-by.pipe';
import { FilterPipe } from './user-actions/pipes/filter.pipe';

export const appRoutes: Routes = [
  { path: 'LogIn/LogIn', component: LogInFormComponent, runGuardsAndResolvers: 'always' },
  { path: 'SignUp/SignUp', component: SignUpFormComponent },
  { path: 'User/Edit/:id', component: EditUserFormComponent, runGuardsAndResolvers: 'always' },
  { path: 'UserAction/GetAll', component: UserActionsComponent, runGuardsAndResolvers: 'always' },
  { path: '**', redirectTo:'/' }
];

@NgModule({
  declarations: [
    AppComponent,
    EditUserFormComponent,
    LogInFormComponent,
    SignUpFormComponent,
    ConfirmPasswordDirective,
    UserActionsComponent,
    SortByPipe,
    FilterPipe,
    //UniqueUserNameValidator
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    DpDatePickerModule,
    AngularFontAwesomeModule,
    RouterModule.forRoot(appRoutes, { onSameUrlNavigation: 'reload' })
  ],
  exports: [RouterModule],
  providers: [
    LogInService,
    UserService,
    SignUpService,
    UserActionService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
