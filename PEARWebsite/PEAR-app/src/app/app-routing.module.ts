import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// import { DashboardComponent } from './views/dashboard/dashboard-list/dashboard.component';
import { LoginComponent } from '../app/views/login/login.component';
import { NavbarComponent } from '../app/components/navbar/navbar.component';
import { RegisterComponent } from '../app/views/register/register.component';
import { SplashpageComponent } from '../app/views/splashpage/splashpage.component';
import { PageNotFoundComponent } from '../app/views/page-not-found/page-not-found.component';
import { ProfileComponent } from './views/profile/profile.component';

const routes2: Routes = [
  // { path: 'dashboard', component: DashboardComponent },
  { path: 'login', component: LoginComponent },
  { path: 'navbar', component: NavbarComponent },
  { path: 'splashpage', component: SplashpageComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'profile', component: ProfileComponent },

  { path: '', redirectTo: '/splashpage', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes2) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
