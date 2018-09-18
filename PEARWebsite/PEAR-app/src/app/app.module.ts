import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { environment } from '../environments/environment';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './/app-routing.module';

import { AngularFireModule } from 'angularfire2';
import { AngularFirestoreModule } from 'angularfire2/firestore';
import { AngularFireDatabaseModule } from 'angularfire2/database';
import { AngularFireAuthModule } from 'angularfire2/auth';

import { TeachersComponent } from '../app/views/teachers/teachers.component';
import { TeacherDetailComponent } from '../app/views/teacher-detail/teacher-detail.component';
import { DashboardComponent } from '../app/views/dashboard/dashboard.component';
import { LoginComponent } from '../app/views/login/login.component';
import { AuthService } from './services/auth.service';
import { SidenavComponent } from './sidenav/sidenav.component';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    NgbModule.forRoot(),
    HttpClientModule,
    AppRoutingModule,
    AngularFireModule.initializeApp(environment.firebase, 'PEAR'),
    AngularFirestoreModule, // imports firebase/firestore, only needed for database features
    AngularFireAuthModule, // imports firebase/auth, only needed for auth features,
  ],
  declarations: [
    AppComponent,
    TeachersComponent,
    TeacherDetailComponent,
    DashboardComponent,
    LoginComponent,
    SidenavComponent
  ],
  providers: [AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
