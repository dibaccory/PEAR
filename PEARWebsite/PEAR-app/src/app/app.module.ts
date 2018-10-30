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
import { DatabaseTestComponent } from '../app/old/database-test/database-test.component';
import { AppRoutingModule } from './app-routing.module';
import { GoogleChartsComponent } from '../app/old/google-charts/google-charts.component';
import { RegisterComponent } from '../app/views/register/register.component';
import { SplashpageComponent } from '../app/views/splashpage/splashpage.component';
import { PageNotFoundComponent } from '../app/views/page-not-found/page-not-found.component';
import { ProfileComponent } from './views/profile/profile.component';
import { DashboardModule } from './views/dashboard/dashboard.module';
import { AuthAF2Service } from './auth-af2.service';

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
    DatabaseTestComponent,
    GoogleChartsComponent,
    RegisterComponent,
    SplashpageComponent,
    PageNotFoundComponent,
    ProfileComponent
  ],
  providers: [AuthAF2Service],
  bootstrap: [AppComponent]
})
export class AppModule { }
