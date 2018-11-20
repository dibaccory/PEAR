import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { environment } from '../environments/environment';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap'; // Bootstrap
import { GoogleChartsModule } from 'angular-google-charts';
import { FormsModule } from '@angular/forms';

// Angularfire2
import { AngularFireModule } from '@angular/fire';
import { AngularFireDatabaseModule } from '@angular/fire/database';
import { AngularFireAuthModule } from '@angular/fire/auth';

// My created components and modules
import { LoginComponent } from '../app/views/login/login.component';
import { NavbarComponent } from '../app/components/navbar/navbar.component';
import { AppRoutingModule } from './app-routing.module';
import { RegisterComponent } from '../app/views/register/register.component';
import { SplashpageComponent } from '../app/views/splashpage/splashpage.component';
import { PageNotFoundComponent } from '../app/views/page-not-found/page-not-found.component';
import { ProfileComponent } from './views/profile/profile.component';
import { DashboardModule } from './views/dashboard/dashboard.module';

// Services
import { ClassCodesService } from './services/class-code.service';
import { TeachersService } from './services/teachers.service';
import { StudentsService } from './services/students.service';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    NgbModule.forRoot(),
    AngularFireModule.initializeApp(environment.firebase, 'PEAR'),
    AngularFireDatabaseModule,
    AngularFireAuthModule,
    GoogleChartsModule,
    DashboardModule,
    AppRoutingModule,
  ],
  declarations: [
    AppComponent,
    LoginComponent,
    NavbarComponent,
    RegisterComponent,
    SplashpageComponent,
    PageNotFoundComponent,
    ProfileComponent
  ],
  providers: [
    ClassCodesService,
    TeachersService,
    StudentsService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
