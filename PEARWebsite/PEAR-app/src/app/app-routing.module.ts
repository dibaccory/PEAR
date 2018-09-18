import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthService } from '../app/services/auth.service';

import { TeachersComponent } from './views/teachers/teachers.component';
import { DashboardComponent } from './views/dashboard/dashboard.component';
import { TeacherDetailComponent } from './views/teacher-detail/teacher-detail.component';
import { LoginComponent } from './views/login/login.component';

const routes: Routes = [
  { path: 'teachers', component: TeachersComponent },
  { path: 'detail/:id', component: TeacherDetailComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'login', component: LoginComponent},
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' }
];

@NgModule({
  exports: [RouterModule],
  imports: [RouterModule.forRoot(routes)]
})
export class AppRoutingModule { }
