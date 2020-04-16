import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { AngularFontAwesomeModule} from 'angular-font-awesome';
import bootstrap from "bootstrap";
import { DpDatePickerModule } from 'ng2-date-picker';
import { CookieService } from 'ngx-cookie-service';

import { AppComponent } from './app.component';
import { EditUserFormComponent } from './edit-user-form/edit-user-form.component';
import { LogInFormComponent } from './log-in-form/log-in-form.component';
import { SignUpFormComponent } from './sign-up-form/sign-up-form.component';
import { UserActionsComponent } from './user-actions/user-actions.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { NavbarComponent } from './navbar/navbar.component';

import { ConfirmPasswordDirective } from './shared/confirm-password.directive';
import { UserService } from './shared/services/user.service';
import { UserActionService } from './user-actions/user-action.service';
import { SortByPipe } from './user-actions/pipes/sort-by.pipe';
import { HttpErrorInterceptor } from './shared/http-error.interceptor';


export const appRoutes: Routes = [
  { path: 'LogIn', component: LogInFormComponent, runGuardsAndResolvers: 'always' },
  { path: 'SignUp', component: SignUpFormComponent },
  { path: 'Users/:id', component: EditUserFormComponent, runGuardsAndResolvers: 'always' },
  { path: 'UserActions', component: UserActionsComponent, runGuardsAndResolvers: 'always' },
  { path: '', redirectTo: 'LogIn', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent }
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
    PageNotFoundComponent,
    NavbarComponent,
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    DpDatePickerModule,
    AngularFontAwesomeModule,
    RouterModule.forRoot(appRoutes, { onSameUrlNavigation: 'reload' })
  ],
  exports: [RouterModule],
  providers: [
    CookieService,
    UserService,
    UserActionService,
    { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
