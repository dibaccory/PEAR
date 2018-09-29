import { NgModule } from '@angular/core';
import { RouterModule, Routes, Router } from '@angular/router';

import { DashboardComponent } from '../app/views/dashboard/dashboard.component';
import { LoginComponent } from '../app/views/login/login.component';
import { SplashpageComponent } from '../app/views/splashpage/splashpage.component';
import { DatabaseTestComponent } from '../app/views/database-test/database-test.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'database', component: DatabaseTestComponent },
  { path: 'login', component: LoginComponent },
  { path: 'splashpage', component: SplashpageComponent },
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
