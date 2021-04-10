import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CarComponent } from './Components/car/car.component'
import { RentalComponent } from './Components/rental/rental.component'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { MatSliderModule } from '@angular/material/slider';
import { MatTableModule } from '@angular/material/table';
import { MatSidenavModule } from '@angular/material/sidenav';
import { NavbarComponent } from './Components/navbar/navbar.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { IvyGalleryModule } from 'angular-gallery';
import { VatAddedPipe } from './pipes/vat-added.pipe';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {MatSelectModule} from '@angular/material/select';
import { FilterPipePipe } from './pipes/filter-pipe.pipe';
import { TryPipePipe } from './pipes/try-pipe.pipe';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatDialogModule, MatDialogRef, MAT_DIALOG_DEFAULT_OPTIONS} from '@angular/material/dialog';
import { RentalAddComponent } from './Components/rental-add/rental-add.component';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { ErrorStateMatcher, MatNativeDateModule, ShowOnDirtyErrorStateMatcher } from '@angular/material/core';
import {MatStepperModule} from '@angular/material/stepper';
import { CreditCardPipe } from './pipes/credit-card.pipe';
import { CarAddComponent } from './Components/car-add/car-add.component';
import { MatFileUploadModule } from 'angular-material-fileupload';
import { LoginComponent } from './Components/login/login.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { DatePipe } from '@angular/common';
import { BnNgIdleService } from 'bn-ng-idle';
import { RouterModule } from '@angular/router';
import { IdleLogoutComponent } from './Components/idle-logout/idle-logout.component';
import { LoadingComponent } from './Components/loading/loading.component';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { RegisterComponent } from './Components/register/register.component';
import { UserComponent } from './Components/user/user.component';
import { UserUpdateComponent } from './Components/user-update/user-update.component';
import { UserClaimsUpdateComponent } from './Components/user-claims-update/user-claims-update.component';
import { FirstcharacterPipe } from './pipes/firstcharacter.pipe';
import { CategoryComponent } from './Components/category/category.component';
import { CategoryUpdateComponent } from './Components/category-update/category-update.component';
import { CategoryAddComponent } from './Components/category-add/category-add.component';
import { ConfirmComponent } from './Components/confirm/confirm.component';
import { ConfirmService } from './Services/confirm.service';
import { LandingPageComponent } from './Components/landing-page/landing-page.component';
import { ForgotPasswordComponent } from './Components/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './Components/reset-password/reset-password.component';
import { AccountActivateComponent } from './Components/account-activate/account-activate.component';


@NgModule({
  declarations: [
    AppComponent,
    CarComponent,
    RentalComponent,
    NavbarComponent,
    DashboardComponent,
    RentalAddComponent,
    CarAddComponent,
    VatAddedPipe,
    FilterPipePipe,
    TryPipePipe,
    CreditCardPipe,
    FirstcharacterPipe,
    LoginComponent,
    IdleLogoutComponent,
    LoadingComponent,
    RegisterComponent,
    UserComponent,
    UserUpdateComponent,
    UserClaimsUpdateComponent,
    CategoryComponent,
    CategoryUpdateComponent,
    CategoryAddComponent,
    ConfirmComponent,
    LandingPageComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    AccountActivateComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    MatSliderModule,
    MatTableModule,
    MatSidenavModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatListModule,
    MatGridListModule,
    MatCardModule,
    MatMenuModule,
    MatPaginatorModule,
    MatSortModule,
    MatTooltipModule,
    MatExpansionModule,
    MatProgressBarModule,
    IvyGalleryModule,
    MatInputModule,
    MatSelectModule,
    MatSnackBarModule,
    MatDialogModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatStepperModule,
    MatFileUploadModule,
    MatCheckboxModule,
  ],
  entryComponents: [
    CarComponent, 
    RentalAddComponent, 
    CarAddComponent, 
    LoginComponent, 
    IdleLogoutComponent, 
    RegisterComponent, 
    UserUpdateComponent, 
    UserClaimsUpdateComponent,
    CategoryComponent,
    CategoryUpdateComponent,
    CategoryAddComponent,
    ConfirmComponent,
    ForgotPasswordComponent,
  ],
  providers: [
    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { appearance: 'legacy' } },
    { provide: HTTP_INTERCEPTORS, useClass:AuthInterceptor, multi: true}, // Auth interceptor will work all operations
    { provide: ErrorStateMatcher, useClass: ShowOnDirtyErrorStateMatcher},
    ConfirmService,
    MatDatepickerModule,
    DatePipe,
    BnNgIdleService
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
