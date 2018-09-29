import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// import { DashboardComponent } from '../app/views/dashboard/dashboard.component';
import { LoginComponent } from '../app/views/login/login.component';
import { SplashpageComponent } from '../app/views/splashpage/splashpage.component';
import { DatabaseTestComponent } from '../app/views/database-test/database-test.component';
import { NgxChartsComponent } from '../app/views/ngx-charts/ngx-charts.component';
import { GoogleChartsComponent } from '../app/views/google-charts/google-charts.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'database', component: DatabaseTestComponent },
  { path: 'login', component: LoginComponent },
  { path: 'splashpage', component: SplashpageComponent },
  { path: 'ngx-charts', component: NgxChartsComponent },
  { path: 'google-charts', component: GoogleChartsComponent }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
