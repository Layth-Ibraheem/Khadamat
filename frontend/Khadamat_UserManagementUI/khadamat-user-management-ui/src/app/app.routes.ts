import {Routes} from '@angular/router';
import {LoginComponent} from './features/auth/components/login/login.component';
import {RegisterComponent} from './features/auth/components/register/register.component';
import {authGuard} from './shared/guards/auth.guard';
import {HomeComponent} from './shared/components/home/home.component';
import {ChangePasswordComponent} from './features/auth/components/change-password/change-password.component';
import {ForgetPasswordComponent} from './features/auth/components/forget-password/forget-password.component';
import {
  ListApplicationsComponent
} from './features/applications/components/list-applications/list-applications.component';

export const routes: Routes = [
  {path: '', redirectTo: '/home', pathMatch: 'full'},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'forgetPassword', component: ForgetPasswordComponent},
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [authGuard],
    children: [
      {
        path: 'changePassword',
        component: ChangePasswordComponent
      },
      {
        path: 'applications',
        component: ListApplicationsComponent
      }
    ]
  },
  {path: '**', component: HomeComponent, canActivate: [authGuard]},
];
