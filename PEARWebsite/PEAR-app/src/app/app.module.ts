import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { environment } from '../environments/environment';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap'; // Bootstrap
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { GoogleChartsModule } from 'angular-google-charts';
import { FormsModule } from '@angular/forms';

// Angularfire2
import { AngularFireModule } from '@angular/fire';
import { AngularFireDatabaseModule } from '@angular/fire/database';
import { AngularFireAuthModule } from '@angular/fire/auth';

// My created components
import { DashboardComponent } from '../app/views/dashboard/dashboard.component';
import { LoginComponent } from '../app/views/login/login.component';
import { NavbarComponent } from '../app/components/navbar/navbar.component';
import { DatabaseTestComponent } from '../app/old/database-test/database-test.component';
import { AppRoutingModule } from './app-routing.module';
import { NgxChartsComponent } from '../app/old/ngx-charts/ngx-charts.component';
import { GoogleChartsComponent } from '../app/old/google-charts/google-charts.component';
import { RegisterComponent } from '../app/views/register/register.component';
import { SplashpageComponent } from '../app/views/splashpage/splashpage.component';
import { QuestionsComponent } from '../app/views/dashboard/questions/questions.component';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    NgbModule.forRoot(),
    AngularFireModule.initializeApp(environment.firebase, 'PEAR'),
    AngularFireDatabaseModule,
    AngularFireAuthModule,
    AppRoutingModule,
    NgxChartsModule,
    GoogleChartsModule
  ],
  declarations: [
    AppComponent,
    DashboardComponent,
    LoginComponent,
    NavbarComponent,
    DatabaseTestComponent,
    NgxChartsComponent,
    GoogleChartsComponent,
    RegisterComponent,
    SplashpageComponent,
    QuestionsComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
