import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { environment } from '../environments/environment';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap'; // Bootstrap
import { FormsModule } from '@angular/forms';

// Angularfire2
import { AngularFireModule } from '@angular/fire';
import { AngularFireDatabaseModule } from '@angular/fire/database';

// My created components
import { DashboardComponent } from '../app/views/dashboard/dashboard.component';
import { LoginComponent } from '../app/views/login/login.component';
import { SplashpageComponent } from '../app/views/splashpage/splashpage.component';
// import { NavbarComponent } from '../app/views/navbar/navbar.component';
import { DatabaseTestComponent } from '../app/views/database-test/database-test.component';
import { AppRoutingModule } from './app-routing.module';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    NgbModule.forRoot(),
    AngularFireModule.initializeApp(environment.firebase, 'PEAR'),
    AngularFireDatabaseModule,
    AppRoutingModule
  ],
  declarations: [
    AppComponent,
    DashboardComponent,
    LoginComponent,
    SplashpageComponent,
    // NavbarComponent,
    DatabaseTestComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
