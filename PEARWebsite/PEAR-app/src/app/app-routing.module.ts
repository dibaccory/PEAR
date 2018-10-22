import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent } from '../app/views/dashboard/dashboard.component';
import { LoginComponent } from '../app/views/login/login.component';
import { NavbarComponent } from '../app/components/navbar/navbar.component';
import { DatabaseTestComponent } from '../app/old/database-test/database-test.component';
import { NgxChartsComponent } from '../app/old/ngx-charts/ngx-charts.component';
import { GoogleChartsComponent } from '../app/old/google-charts/google-charts.component';
import { RegisterComponent } from '../app/views/register/register.component';
import { SplashpageComponent } from '../app/views/splashpage/splashpage.component';
import { QuestionsComponent } from '../app/views/dashboard/questions/questions.component';

const routes: Routes = [
  { path: '', redirectTo: '/splashpage', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'database', component: DatabaseTestComponent },
  { path: 'login', component: LoginComponent },
  { path: 'navbar', component: NavbarComponent },
  { path: 'splashpage', component: SplashpageComponent },
  { path: 'ngx-charts', component: NgxChartsComponent },
  { path: 'google-charts', component: GoogleChartsComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'questions', component: QuestionsComponent }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
