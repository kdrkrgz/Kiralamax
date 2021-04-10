import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { AccountActivateComponent } from './Components/account-activate/account-activate.component';
import { CarComponent } from './Components/car/car.component';
import { CategoryComponent } from './Components/category/category.component';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { LandingPageComponent } from './Components/landing-page/landing-page.component';
import { RentalComponent } from './Components/rental/rental.component';
import { ResetPasswordComponent } from './Components/reset-password/reset-password.component';
import { UserComponent } from './Components/user/user.component';
import { LoginGuard } from './guards/login.guard';

const routes: Routes = [

  {path:"dashboard", component: DashboardComponent, canActivate:[LoginGuard]},
  {path:"categories", component: CategoryComponent, canActivate:[LoginGuard]},
  {path:"cars", component: CarComponent},
  {path:"rentals", component: RentalComponent, canActivate:[LoginGuard]},
  {path:"users", component: UserComponent, canActivate:[LoginGuard]},
  {path:"resetpassword/:useremail/:activationCode", component: ResetPasswordComponent},
  {path:"activateaccount/:useremail/:activationCode", component: AccountActivateComponent},
  {path: '',  redirectTo: '/',  pathMatch: 'full'}
  // {path:"activateuser/:useremail/:activationCode", component: ResetPasswordComponent},
  // { path: '', component: LandingPageComponent},
  // { path: '**', component: LandingPageComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
